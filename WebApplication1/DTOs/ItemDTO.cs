using WebApplication1.Enums;
using WebApplication1.Models;
public class ItemDTO
{
    public ItemType ItemType { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string? Brand { get; set; }
    public bool IsAvailable { get; set; }
    public string? TitleImageUrl { get; set; }
    public List<string>? AdditionalImageUrls { get; set; } = new List<string>();

 // Exclude TitleImage and other properties you want to ignore
}
