namespace WebApplication1.DTOs

{
    
public class OrderItemDto
{
    public Guid OrderedItemGUID { get; set; } // GUID of the ordered item
    public string? OriginalItemGUID { get; set; } // GUID of the original item
    public string? Title { get; set; }
    public string? TitleImageUrl { get; set; }
    public int QuantityToPurchase { get; set; }
    public decimal Price { get; set; }
}

}
