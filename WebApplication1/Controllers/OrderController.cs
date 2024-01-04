using Microsoft.AspNetCore.Mvc;
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




}

