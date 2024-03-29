using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models
{
    public class Order
    {
        public Guid OrderId { get; set; } // Unique order number
        public string? UserGUID { get; set; } // Email of the user placing the order
        public string? UserName { get; set; } // Name of the user placing the order
        public decimal TotalPrice { get; set; } // Total price of the order
        public OrderStatus Status { get; set; } = OrderStatus.Pending; // Default status is Pending
        public List<OrderItem>? Items { get; set; } // Items in the order
        public DateTime DatePlaced { get; set; } = DateTime.UtcNow; // Date and time when the order was placed

        // Add other relevant properties like OrderDate, Status, etc.
    }

    public class OrderItem
    {
        [Key]
       public Guid OrderedItemGUID { get; set; } // GUID of the ordered item
        public string? OriginalItemGUID { get; set; } // GUID of the original item
        public string? Title { get; set; }
        public string? TitleImageUrl { get; set; }
        public int QuantityToPurchase { get; set; }
        public decimal Price { get; set; }
        // Add other relevant properties as needed
    }
}

