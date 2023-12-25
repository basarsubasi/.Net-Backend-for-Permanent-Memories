using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.DTOs;
using WebApplication1.Enums;
using AutoMapper;
using System;

namespace WebApplication1.Controllers
{

[ApiController]
[Route("api/[Controller]")]
public class ItemController : ControllerBase
{
    private readonly AppDbContext _dbContext;
   
    public ItemController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


[HttpGet("searchItems")]
public async Task<IActionResult> SearchItems(
    [FromQuery] bool IWantToFilter = false,
    [FromQuery] ItemType? itemType = null,
    [FromQuery] Guid? guid = null,
    [FromQuery] string? title = null,
    [FromQuery] bool? isAvailable = null,
    [FromQuery] decimal? minPrice = null,
    [FromQuery] decimal? maxPrice = null,
    [FromQuery] string? brand = null)
{
    try
    {
        IQueryable<Item> itemsQuery = _dbContext.Items;

        if (IWantToFilter)
        {
            if (itemType.HasValue)
            {
                itemsQuery = itemsQuery.Where(item => item.ItemType == itemType);
            }

            if (guid.HasValue)
            {
                itemsQuery = itemsQuery.Where(item => item.GUID == guid);
            }

            if (!string.IsNullOrEmpty(title))
            {
                itemsQuery = itemsQuery.Where(item => item.Title != null && item.Title.ToUpper().Contains(title.ToUpper()));            }

            if (isAvailable.HasValue)
            {
                itemsQuery = itemsQuery.Where(item => item.IsAvailable == isAvailable.Value);
            }

            if (minPrice.HasValue)
            {
                itemsQuery = itemsQuery.Where(item => item.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                itemsQuery = itemsQuery.Where(item => item.Price <= maxPrice.Value);
            }

            if (!string.IsNullOrEmpty(brand))
            {
                 itemsQuery = itemsQuery.Where(item => item.Brand != null && item.Brand.ToUpper().Contains(brand.ToUpper()));
            }
        }

        var items = await itemsQuery
            .Select(item => new
            {
                item.Title,
                item.Description,
                item.Quantity,
                item.Price,
                item.Brand,
                item.IsAvailable,
                item.GUID,
                item.ItemType,
            })
            .ToListAsync();

        return Ok(items);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error in SearchItems: {ex}");
        return StatusCode(500, "Internal Server Error");
    }
}





[HttpGet("getTitleImage/{guid}")]
public async Task<IActionResult> GetTitleImageByGuid(Guid guid)
{
    try
    {
        var item = await _dbContext.Items.FirstOrDefaultAsync(i => i.GUID == guid);

        if (item == null)
        {
            return NotFound($"Item with GUID {guid} not found");
        }

        if (string.IsNullOrEmpty(item.TitleImageUrl))
        {
            return NotFound($"Image not found for item with GUID {guid}");
        }

        using (var httpClient = new HttpClient())
        {
            try
            {
                var imageBytes = await httpClient.GetByteArrayAsync(item.TitleImageUrl);

                if (imageBytes.Length > 0)
                {
                    return File(imageBytes, "image/jpeg");
                }
                else
                {
                    return NotFound($"Image not found for item with GUID {guid}");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
    catch (Exception)
    {
        return StatusCode(500, "Internal Server Error");
    }
}

[HttpGet("getAdditionalImage/{guid}")]
public async Task<IActionResult> GetAdditionalImage(Guid guid, [FromQuery] int index)
{
    try
    {
        var item = await _dbContext.Items.FirstOrDefaultAsync(i => i.GUID == guid);

        if (item == null)
        {
            return NotFound($"Item with GUID {guid} not found");
        }

        if (item.AdditionalImageUrls == null || item.AdditionalImageUrls.Count == 0)
        {
            return NotFound($"No additional images found for item with GUID {guid}");
        }

        if (index < 0 || index >= item.AdditionalImageUrls.Count)
        {
            return BadRequest("Invalid index parameter");
        }

        using (var httpClient = new HttpClient())
        {
            try
            {
                var imageUrl = item.AdditionalImageUrls[index];
                var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);

                if (imageBytes.Length > 0)
                {
                    return File(imageBytes, "image/jpeg");
                }
                else
                {
                    return NotFound($"Image not found for item with GUID {guid} at index {index}");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
    catch (Exception)
    {
        return StatusCode(500, "Internal Server Error");
    }
}






[HttpGet("{guid}")]
public IActionResult GetItemByGuid(Guid guid)
{
    try
    {
        // Retrieve the item based on the provided GUID
        var item = _dbContext.Items.FirstOrDefault(i => i.GUID == guid);

        if (item == null)
        {
            return NotFound($"Item with GUID {guid} not found");
        }

        return Ok(item);
    }
    catch (Exception)
    {
        // Log the exception
        return StatusCode(500, "Internal Server Error");
    }
}





[HttpPut("editItem/{guid}")]
public IActionResult EditItem(Guid guid, [FromBody] EditItemDTO editItemDTO)
{
    try
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var itemToUpdate = _dbContext.Items.FirstOrDefault(i => i.GUID == guid);

        if (itemToUpdate == null)
        {
            return NotFound("Item not found");
        }
        var ItemDetails = editItemDTO.ItemDetails;

        if (ItemDetails != null)
        {
            // Update common properties
            itemToUpdate.Title = ItemDetails.Title ?? itemToUpdate.Title;
            itemToUpdate.Description = ItemDetails.Description ?? itemToUpdate.Description;
            itemToUpdate.Quantity = ItemDetails.Quantity > 0 ? ItemDetails.Quantity : itemToUpdate.Quantity;
            itemToUpdate.Price = ItemDetails.Price > 0 ? ItemDetails.Price : itemToUpdate.Price;
            itemToUpdate.Brand = editItemDTO.ItemDetails.Brand ?? itemToUpdate.Brand;
            itemToUpdate.IsAvailable = editItemDTO.ItemDetails.IsAvailable;
            itemToUpdate.TitleImageUrl = editItemDTO.ItemDetails.TitleImageUrl ?? itemToUpdate.TitleImageUrl;
            itemToUpdate.AdditionalImageUrls = editItemDTO.ItemDetails.AdditionalImageUrls ?? itemToUpdate.AdditionalImageUrls;

        }
       
        // Update specific details based on item type
        switch (editItemDTO.ItemDetails.ItemType)
        {
            case ItemType.Film:
                // Your existing film-specific updates here
                var filmDetails = editItemDTO.FilmDetails;
                if (filmDetails != null)
                {
                    if (itemToUpdate is Film film)
                    {
                        film.FilmColorState = filmDetails.FilmColorState;
                        film.FilmSize = filmDetails.FilmSize;
                        film.FilmISO = filmDetails.FilmISO;
                        film.FilmExposure = filmDetails.FilmExposure;
                    }
                    else
                    {
                        // Handle the case where the item type in the database is not a Film
                        return BadRequest("Invalid item type");
                    }
                }
                break;

            // Add cases for other item types as needed

            default:
                return BadRequest("Unsupported item type");
        }

        _dbContext.SaveChanges();

        return Ok("Item updated successfully");
    }
    catch (Exception)
    {
        // Log the exception
        return StatusCode(500, "Internal Server Error");
    }
}







    


    [HttpPost("createItem/{itemType}")]
public IActionResult CreateItem(string itemType, [FromBody] CreateItemDTO createItemDTO)
{
    try
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Parse the itemType string to ItemType enum
        if (Enum.TryParse(itemType, true, out ItemType parsedItemType))
        {
            switch (parsedItemType)
            {
                case ItemType.Film:
                    var filmDetails = createItemDTO.FilmDetails;
                    var ItemDetails = createItemDTO.ItemDetails;

                    if (filmDetails != null)
                    {
                        var newFilm = new Film
                        {
                            Title = ItemDetails.Title ?? "Default Title",
                            Description = ItemDetails.Description ?? "Default Description",
                            Quantity = ItemDetails.Quantity > 0 ? ItemDetails.Quantity : 1,
                            Price = ItemDetails.Price > 0 ? ItemDetails.Price : 0.0m,
                            Brand = ItemDetails.Brand ?? "Default Brand",
                            IsAvailable = ItemDetails.IsAvailable,
                            TitleImageUrl = ItemDetails.TitleImageUrl ?? "Default Title Image URL",
                            AdditionalImageUrls = ItemDetails.AdditionalImageUrls ?? new List<string>(),
                            FilmColorState = filmDetails.FilmColorState,
                            FilmSize = filmDetails.FilmSize,
                            FilmISO = filmDetails.FilmISO,
                            FilmExposure = filmDetails.FilmExposure

                            // Set other film properties as needed
                        };

                        // Add the new film to the database
                        _dbContext.Items.Add(newFilm);
                        _dbContext.SaveChanges();

                       

                        return Ok("Film created successfully");
                    }
                    break;

                // Add cases for other item types as needed

                default:
                    return BadRequest("Unsupported item type");
            }
        }
        else
        {
            return BadRequest("Invalid item type");
        }
    }
    catch (Exception)
    {
        // Log the exception
        return StatusCode(500, "Internal Server Error");
    }

    return BadRequest("Invalid request");
}

[HttpDelete("deleteItems")]
public IActionResult DeleteItems(
    [FromQuery] Guid? guid = null,
    [FromQuery] ItemType? itemType = null)
{
    try
    {
        IQueryable<Item> itemsToDelete = _dbContext.Items;

        // Apply filters based on query parameters
        if (guid.HasValue)
        {
            itemsToDelete = itemsToDelete.Where(item => item.GUID == guid.Value);
        }

        if (itemType.HasValue)
        {
            itemsToDelete = itemsToDelete.Where(item => item.ItemType == itemType.Value);
        }

        // Execute the query and delete the items
        var items = itemsToDelete.ToList();
        _dbContext.Items.RemoveRange(items);
        _dbContext.SaveChanges();

        return Ok("Items deleted successfully");
    }
    catch (Exception)
    {
        // Log the exception
        return StatusCode(500, "Internal Server Error");
    }
}
    
    }
    }   






    
