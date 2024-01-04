using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models; // Replace with your actual namespace
using WebApplication1.Data; // Replace with the namespace of your OrderDbContext
using WebApplication1.DTOs;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderDbContext _dbContext;

    public OrderController(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

   [HttpPost("createOrder")]
   [Authorize]
public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
{
    try
    {
        // Check if Items is null and initialize it if necessary
        var items = orderDto.Items ?? new List<OrderItemDto>();

        var order = new Order
        {
            OrderId = Guid.NewGuid(),
            UserGUID = orderDto.UserGUID,
            DatePlaced = DateTime.UtcNow,
            UserName = orderDto.UserName,
            TotalPrice = orderDto.TotalPrice,
            Status = orderDto.Status,
            // Use the non-null items list here
            Items = items.Select(i => new OrderItem

            {
                OrderedItemGUID = Guid.NewGuid(),
                OriginalItemGUID = i.OriginalItemGUID,
                Title = i.Title,
                TitleImageUrl = i.TitleImageUrl,
                QuantityToPurchase = i.QuantityToPurchase,
                Price = i.Price
            }).ToList()
        };

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        return Ok(new { OrderId = order.OrderId });
    }
    catch (Exception ex)
    {
        // Log the exception
        return StatusCode(500, "Internal Server Error: " + ex.Message);
    }
}

[HttpGet("listOrders")]
[Authorize]
public IActionResult ListOrders([FromQuery] OrderFilterDto filter)
{
    try
    {
        var query = _dbContext.Orders.AsQueryable();

        // Filter by UserName if provided
        if (!string.IsNullOrWhiteSpace(filter.UserName))
        {
            query = query.Where(o => o.UserName == filter.UserName);
        }

        // Filter by OrderDate if provided
        if (filter.OrderDate.HasValue)
        {
            query = query.Where(o => o.DatePlaced.Date == filter.OrderDate.Value.Date);
        }

        // Filter by Price range if provided
        if (filter.MinPrice.HasValue)
        {
            query = query.Where(o => o.TotalPrice >= filter.MinPrice.Value);
        }
        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(o => o.TotalPrice <= filter.MaxPrice.Value);
        }

        // Apply sorting
        query = filter.SortOrder.ToLower() switch
        {
            "descending" => query.OrderByDescending(o => o.DatePlaced),
            _ => query.OrderBy(o => o.DatePlaced), // Default to ascending
        };

       var orders = query.ToList();

        // Check if any orders are found
        if (!orders.Any())
        {
            return NotFound("No orders found matching the specified criteria.");
        }

        return Ok(orders);
    }
    catch (Exception ex)
    {
        // Log the exception
        return StatusCode(500, "Internal Server Error: " + ex.Message);
    }
}


[HttpGet("listUserOrders")]
[Authorize]
public IActionResult ListUserOrders([FromQuery] string userGuid)
{
    try
    {
        var query = _dbContext.Orders.AsQueryable();

        // Filter by UserGUID
        query = query.Where(o => o.UserGUID == userGuid);

        // Add other filters and sorting logic as needed here...

        var orders = query.ToList();

        // Check if any orders are found
        if (!orders.Any())
        {
            return NotFound("No orders found for the specified user.");
        }

        return Ok(orders);
    }
    catch (Exception ex)
    {
        // Log the exception
        return StatusCode(500, "Internal Server Error: " + ex.Message);
    }
}



[HttpGet("getOrder/{OrderId}")]
[Authorize]
public IActionResult GetOrder(Guid OrderId)
{
    try
    {
        var order = _dbContext.Orders
            .Include(o => o.Items) 
            .FirstOrDefault(o => o.OrderId == OrderId);

        if (order == null)
        {
            return NotFound($"Order with ID {OrderId} not found.");
        }

        return Ok(order);
    }
    catch (Exception ex)
    {
        // Log the exception
        return StatusCode(500, "Internal Server Error: " + ex.Message);
    }
}

[HttpPut("updateOrderStatus/{OrderId}")]
[Authorize(Policy = "EmployeeOrAdmin")]
public async Task<IActionResult> UpdateOrderStatus(Guid OrderId, [FromBody] OrderStatus newStatus)
{
    try
    {
        var order = await _dbContext.Orders.FindAsync(OrderId);

        if (order == null)
        {
            return NotFound($"Order with ID {OrderId} not found.");
        }

        order.Status = newStatus; // Update the status
        await _dbContext.SaveChangesAsync();

        return Ok($"Order status updated to {newStatus}.");
    }
    catch (Exception ex)
    {
        // Log the exception
        return StatusCode(500, "Internal Server Error: " + ex.Message);
    }
}



[HttpDelete("deleteOrder/{OrderId}")]
[Authorize(Policy = "AdminOnly")]
public async Task<IActionResult> DeleteOrder(Guid OrderId)
{
    try
    {
        var order = await _dbContext.Orders.FindAsync(OrderId);

        if (order == null)
        {
            return NotFound($"Order with ID {OrderId} not found.");
        }

        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();

        return Ok($"Order with ID {OrderId} has been deleted.");
    }
    catch (Exception ex)
    {
        // Log the exception
        return StatusCode(500, "Internal Server Error: " + ex.Message);
    }
}







}

