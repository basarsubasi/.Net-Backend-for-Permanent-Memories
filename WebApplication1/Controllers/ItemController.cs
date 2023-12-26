using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Enums.ItemEnums;


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
    [FromQuery] string? title = null,
    [FromQuery] bool? isAvailable = null,
    [FromQuery] decimal? minPrice = null,
    [FromQuery] decimal? maxPrice = null,
    [FromQuery] string? brand = null,
    [FromQuery] ItemBrandId? itemBrandId = null,
    [FromQuery] string? sortBy = null, // Sorting parameter
    [FromQuery] bool descending = false) // Sorting order
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
                itemsQuery = itemsQuery.Where(item => item.Brand != null && item.Brand.ToUpper().Contains(brand.ToUpper()));
            }
            if (itemBrandId.HasValue)
            {
                itemsQuery = itemsQuery.Where(item => item.ItemBrandId == itemBrandId);
            }

        }

        // Apply sorting
        if (!string.IsNullOrEmpty(sortBy))
        {
            itemsQuery = ApplySorting(itemsQuery, sortBy, descending);
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
    catch (Exception)
    {

        return StatusCode(500, "Internal Server Error");
    }
}

private IQueryable<Item> ApplySorting(IQueryable<Item> query, string sortBy, bool descending)
{
    
    switch (sortBy.ToLower())
    {
    
        case "title":
            query = descending ? query.OrderByDescending(item => item.Title) : query.OrderBy(item => item.Title);
            break;
        case "price":
            query = descending ? query.OrderByDescending(item => item.Price) : query.OrderBy(item => item.Price);
            break;
        case "brand":
            query = descending ? query.OrderByDescending(item => item.Brand) : query.OrderBy(item => item.Brand);
            break;
        case "quantity":
            query = descending ? query.OrderByDescending(item => item.Quantity) : query.OrderBy(item => item.Quantity);
            break;
    
        // Add more cases for other properties you want to support for sorting
        default:
            // Handle invalid sortBy parameter
            break;
    }
    

    return query;
}





[HttpGet("getTitleImage/{guid}")]
public async Task<IActionResult> GetTitleImageByGuid(Guid guid)
{
    try
    {
        var item = await _dbContext.Items.FirstOrDefaultAsync(i => i.GUID == guid);

        if (item == null)
        {
            return NotFound("Item not found");
        }

        if (string.IsNullOrEmpty(item.TitleImageUrl))
        {
            return NotFound("Image not found for item");
        }

        using (var httpClient = new HttpClient())
        {
            try
            {
                var imageBytes = await httpClient.GetByteArrayAsync(item.TitleImageUrl);

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    return File(imageBytes, "image/jpeg");
                }

              else
              {
                return NotFound($"Image not found for item with GUID {guid}");
              }
   
            }

            catch (HttpRequestException)
    {
        // Handle invalid URL exception
            return NotFound("Invalid image URL for item");
    }
            catch (Exception)
            {
                return StatusCode(500, "Error fetching image for item");
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

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    return File(imageBytes, "image/jpeg");
                }
                else
                {
                    return NotFound($"Image not found for item with GUID {guid} at index {index}");
                }
            }
            catch (HttpRequestException)
            {
                return NotFound($"Image not found for item with GUID {guid} at index {index}");
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





[HttpPut("editFilm/{guid}")]
public IActionResult EditFilm(Guid guid, [FromBody] EditItemDTO editItemDTO)
{
    try
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var filmToUpdate = _dbContext.Items.OfType<Film>().FirstOrDefault(f => f.GUID == guid);

        if (filmToUpdate == null)
        {
            return NotFound("Film not found");
        }

        var itemDetails = editItemDTO.ItemDetails;
        var filmDetails = editItemDTO.FilmDetails;

        if (itemDetails != null)
        {
            // Update common properties
            filmToUpdate.Title = itemDetails.Title ?? filmToUpdate.Title;
            filmToUpdate.Description = itemDetails.Description ?? filmToUpdate.Description;
            filmToUpdate.Quantity = itemDetails.Quantity > 0 ? itemDetails.Quantity : filmToUpdate.Quantity;
            filmToUpdate.Price = itemDetails.Price > 0 ? itemDetails.Price : filmToUpdate.Price;
            filmToUpdate.Brand = itemDetails.Brand ?? filmToUpdate.Brand;
            filmToUpdate.ItemBrandId = editItemDTO.ItemDetails.ItemBrandId; // Update ItemBrandId enum
            filmToUpdate.IsAvailable = itemDetails.IsAvailable;
            filmToUpdate.TitleImageUrl = itemDetails.TitleImageUrl ?? filmToUpdate.TitleImageUrl;
            filmToUpdate.AdditionalImageUrls = itemDetails.AdditionalImageUrls ?? filmToUpdate.AdditionalImageUrls;

            // Update film-specific properties
            filmToUpdate.FilmColorState = editItemDTO.FilmDetails.FilmColorState;
            filmToUpdate.FilmFormat = editItemDTO.FilmDetails.FilmFormat;
            filmToUpdate.FilmISO = editItemDTO.FilmDetails.FilmISO;
            filmToUpdate.FilmExposure = editItemDTO.FilmDetails.FilmExposure;
        }

        _dbContext.SaveChanges();

        return Ok("Film updated successfully");
    }
    catch (Exception)
    {
        // Log the exception
        return StatusCode(500, "Internal Server Error");
    }
}




[HttpPut("editCamera/{guid}")]
public IActionResult EditCamera(Guid guid, [FromBody] EditItemDTO editItemDTO)
{
    try
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cameraToUpdate = _dbContext.Items.OfType<Camera>().FirstOrDefault(c => c.GUID == guid);

        if (cameraToUpdate == null)
        {
            return NotFound("Camera not found");
        }

        var itemDetails = editItemDTO.ItemDetails;
        var cameraDetails = editItemDTO.CameraDetails;

        if (itemDetails != null)
        {
            // Update common properties
            cameraToUpdate.Title = itemDetails.Title ?? cameraToUpdate.Title;
            cameraToUpdate.Description = itemDetails.Description ?? cameraToUpdate.Description;
            cameraToUpdate.Quantity = itemDetails.Quantity > 0 ? itemDetails.Quantity : cameraToUpdate.Quantity;
            cameraToUpdate.Price = itemDetails.Price > 0 ? itemDetails.Price : cameraToUpdate.Price;
            cameraToUpdate.Brand = itemDetails.Brand ?? cameraToUpdate.Brand;
            cameraToUpdate.ItemBrandId = editItemDTO.ItemDetails.ItemBrandId; // Update ItemBrandId enum
            cameraToUpdate.IsAvailable = itemDetails.IsAvailable;
            cameraToUpdate.TitleImageUrl = itemDetails.TitleImageUrl ?? cameraToUpdate.TitleImageUrl;
            cameraToUpdate.AdditionalImageUrls = itemDetails.AdditionalImageUrls ?? cameraToUpdate.AdditionalImageUrls;

            // Update camera-specific properties
            cameraToUpdate.CameraFocalLength = editItemDTO.CameraDetails.CameraFocalLength;
            cameraToUpdate.CameraMaxShutterSpeed = editItemDTO.CameraDetails.CameraMaxShutterSpeed;
            cameraToUpdate.CameraMegapixel = editItemDTO.CameraDetails.CameraMegapixel;
            cameraToUpdate.CameraFilmFormat = editItemDTO.CameraDetails.CameraFilmFormat;
        }

        _dbContext.SaveChanges();

        return Ok("Camera updated successfully");
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
                    var filmItemDetails = createItemDTO.ItemDetails;

                    if (filmDetails != null)
                    {
                        var newFilm = new Film
                        {
                            Title = filmItemDetails.Title ?? "Default Title",
                            Description = filmItemDetails.Description ?? "Default Description",
                            Quantity = filmItemDetails.Quantity > 0 ? filmItemDetails.Quantity : 1,
                            Price = filmItemDetails.Price > 0 ? filmItemDetails.Price : 0.0m,
                            Brand = filmItemDetails.Brand ?? "Default Brand",
                            ItemBrandId = filmItemDetails.ItemBrandId,
                            IsAvailable = filmItemDetails.IsAvailable,
                            ItemType = filmItemDetails.ItemType = ItemType.Film,
                            TitleImageUrl = filmItemDetails.TitleImageUrl ?? "Default Title Image URL",
                            AdditionalImageUrls = filmItemDetails.AdditionalImageUrls ?? new List<string>(),
                            FilmColorState = filmDetails.FilmColorState,
                            FilmFormat = filmDetails.FilmFormat,
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

                case ItemType.Camera:
                    var cameraDetails = createItemDTO.CameraDetails;
                    var cameraItemDetails = createItemDTO.ItemDetails;

                    if (cameraDetails != null)
                    {
                        var newCamera = new Camera
                        {
                            Title = cameraItemDetails.Title ?? "Default Title",
                            Description = cameraItemDetails.Description ?? "Default Description",
                            Quantity = cameraItemDetails.Quantity > 0 ? cameraItemDetails.Quantity : 1,
                            Price = cameraItemDetails.Price > 0 ? cameraItemDetails.Price : 0.0m,
                            Brand = cameraItemDetails.Brand ?? "Default Brand",
                            ItemBrandId = cameraItemDetails.ItemBrandId,
                            IsAvailable = cameraItemDetails.IsAvailable,
                            ItemType = cameraItemDetails.ItemType  = ItemType.Camera,
                            TitleImageUrl = cameraItemDetails.TitleImageUrl ?? "Default Title Image URL",
                            AdditionalImageUrls = cameraItemDetails.AdditionalImageUrls ?? new List<string>(),
                            CameraFocalLength = cameraDetails.CameraFocalLength,
                            CameraMaxShutterSpeed = cameraDetails.CameraMaxShutterSpeed,
                            CameraMegapixel = cameraDetails.CameraMegapixel,
                            CameraFilmFormat = cameraDetails.CameraFilmFormat

                            // Set other camera properties as needed
                        };

                        // Add the new camera to the database
                        _dbContext.Items.Add(newCamera);
                        _dbContext.SaveChanges();

                        return Ok("Camera created successfully");
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






    
