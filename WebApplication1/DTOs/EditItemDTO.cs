using WebApplication1.Enums;
using WebApplication1.DTOs;

public class EditItemDTO
{
    public ItemDTO ItemDetails { get; set; } = new ItemDTO();
    public FilmDTO FilmDetails { get; set; } = new FilmDTO();
    

    // Exclude TitleImage and other properties you want to ignore
}
