
namespace WebApplication1.DTOs

{
public class OrderDto
{
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending; // Include status in DTO
    public List<OrderItemDto>? Items { get; set; }
}

}
