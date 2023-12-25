using System.ComponentModel.DataAnnotations;
using WebApplication1.Enums;
using WebApplication1.Models;

namespace WebApplication1.Models;
public class Item
{ 
    [Key]
    public Guid GUID { get; set; }
    public ItemType ItemType { get; set; }
    public string? TitleImageUrl { get; set; }
    public  List<string> AdditionalImageUrls { get; set; } = new List<string>();
    public string Title { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }

            // Price and availability
    public decimal Price { get; set; }
    public string Brand { get; set; }
    public bool IsAvailable { get; set; }


     public Item()
        {
            // Set default values
            CreateGuid();
            Title = "Default Title";
            Brand = "Default Brand";
        }

       private void CreateGuid()
       {
         // Generate a new GUID when a film instance is created
         GUID = Guid.NewGuid();
       } 
}