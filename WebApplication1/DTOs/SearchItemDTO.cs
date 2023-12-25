using WebApplication1.Enums;
using WebApplication1.DTOs;

public class SearchItemDTO
{
    public bool IWantToFilter { get; set; } = false;
    
    public ItemDTO ItemDetails { get; set; } = new ItemDTO();
    
    public FilmDTO FilmDetails { get; set; } = new FilmDTO();

    // Add missing properties
    public ItemType? ItemType { get; set; }
    public Guid? Guid { get; set; }

    public bool? IsAvailable { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
  
}
