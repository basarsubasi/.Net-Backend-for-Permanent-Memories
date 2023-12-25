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
public IActionResult SearchItems(
    [FromQuery] bool? IWantToFilter = false,
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

        // Apply IWantToFilters based on query parameters if IWantToFilter is true
        if (IWantToFilter == true)
        {
            if (itemType.HasValue)
            {
                itemsQuery = itemsQuery.Where(item => item.ItemType == itemType);
            }


            if (!string.IsNullOrEmpty(title))
            {
                itemsQuery = itemsQuery.Where(item => item.Title != null && item.Title.ToUpper().Contains(title.ToUpper()));
            }

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
                // Assuming Brand is a property in the Item entity
                itemsQuery = itemsQuery.Where(item => item.Brand != null && item.Brand.ToUpper().Contains(brand.ToUpper()));
            }
        }

        // Execute the query and return the results
        var items = itemsQuery.ToList();

        if (items.Count == 0)
        {
            return NotFound("No items found matching the search criteria");
        }

        return Ok(items);
    }
    catch (Exception)
    {
        // Log the exception
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






    // Common endpoint for all items to upload an image
    [HttpPost("uploadTitleImage/{guid}")]
    public IActionResult UploadImage(Guid guid, [FromBody] string imagePath)
    {
         try
    {
        // Validate the image path or perform any necessary checks

        byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);

        var item = _dbContext.Items.FirstOrDefault(i => i.GUID == guid);

        if (item == null)
        {
            return NotFound("Item not found");
        }

        // Add the additional image data to the list
        item.TitleImage=imageBytes;

        _dbContext.SaveChanges();

        return Ok("Title image uploaded successfully");
    }
    catch (Exception)
    {
        // Log the exception
        return StatusCode(500, "Internal Server Error");
    }



    }

    [HttpPost("uploadAdditionalImage/{guid}")]
public IActionResult UploadAdditionalImage(Guid guid, [FromBody] string imagePath)
{
    try
    {
        // Validate the image path or perform any necessary checks

        byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);

        // Assuming you have an Item entity with a property AdditionalImages as List<byte[]>
        var item = _dbContext.Items.FirstOrDefault(i => i.GUID == guid);

        if (item == null)
        {
            return NotFound("Item not found");
        }

        // Add the additional image data to the list
        item.AdditionalImages.Add(imageBytes);

        _dbContext.SaveChanges();

        return Ok("Additional image uploaded successfully");
    }
    catch (Exception)
    {
        // Log the exception
        return StatusCode(500, "Internal Server Error");
    }
}

    // GET: api/items/getTitleImage/{guid}
    [HttpGet("getTitleImage/{guid}")]
    public IActionResult GetTitleImage(Guid guid)
    {
        try
        {
            var item = _dbContext.Items.FirstOrDefault(i => i.GUID == guid);

            if (item == null)
            {
                return NotFound("Item not found");
            }

            if (item.TitleImage == null)
            {
                return NotFound("Title image not available for this item");
            }

            return File(item.TitleImage, "image/png"); // Assuming the image type is PNG
        }
        catch (Exception)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    


    

    [HttpGet("getAdditionalImage/{guid}/{index}")]
public IActionResult GetAdditionalImage(Guid guid, int index)
{
    try
    {
        var item = _dbContext.Items.FirstOrDefault(i => i.GUID == guid);

        if (item == null || index < 0 || index >= item.AdditionalImages.Count)
        {
            return NotFound("Image not found");
        }

        // Retrieve the specific additional image by index
        var imageBytes = item.AdditionalImages[index];

        // Determine the content type based on the image format (e.g., "image/jpeg" or "image/png")
        var contentType = "image/png"; // You may need to adjust this based on your actual image format

        // Return the image data with the appropriate content type
        return File(imageBytes, contentType);
    }
    catch (Exception)
    {
        // Log the exception
        return StatusCode(500, "Internal Server Error");
    }
    
}

// DELETE: api/items/deleteTitleImage/{guid}
    [HttpDelete("deleteTitleImage/{guid}")]
    public IActionResult DeleteTitleImage(Guid guid)
    {
        try
        {
            var itemToDeleteImage = _dbContext.Items.FirstOrDefault(i => i.GUID == guid);

            if (itemToDeleteImage == null)
            {
                return NotFound("Item not found");
            }
            if (itemToDeleteImage.TitleImage == null)
            {
                return NotFound("There is no title image to delete");
            }

            // Delete the title image
            itemToDeleteImage.TitleImage = null;

            _dbContext.SaveChanges();

            return Ok("Title image deleted successfully");
        }
        catch (Exception)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    // DELETE: api/items/deleteAdditionalImage/{guid}/{index}
    [HttpDelete("deleteAdditionalImage/{guid}/{index}")]
    public IActionResult DeleteAdditionalImage(Guid guid, int index)
    {
        try
        {
            var itemToDeleteImage = _dbContext.Items.FirstOrDefault(i => i.GUID == guid);

            if (itemToDeleteImage == null)
            {
                return NotFound("Item not found");
            }

            // Check if the index is within the range of AdditionalImages
            if (index >= 0 && index < itemToDeleteImage.AdditionalImages.Count)
            {
                // Delete the additional image at the specified index
                itemToDeleteImage.AdditionalImages.RemoveAt(index);

                _dbContext.SaveChanges();

                return Ok($"Additional image at index {index} deleted successfully");
            }
            if (itemToDeleteImage.AdditionalImages.Count==0)
            {
                return NotFound("There is no additional image to delete");
            }

            else 
            {
                return BadRequest("Invalid index");
            }
        }
        catch (Exception)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

  
}

}
