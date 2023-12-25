using WebApplication1.Enums;
using WebApplication1.DTOs;
using WebApplication1.Models;

public class CreateItemDTO
{
    public ItemDTO ItemDetails { get; set; } = new ItemDTO();
    public FilmDTO FilmDetails { get; set; } = new FilmDTO();
    

    // Exclude TitleImage and other properties you want to ignore
}
