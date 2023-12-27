using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.ItemRelatedModels;
using WebApplication1.Enums.ItemEnums;
using Microsoft.AspNetCore.Authorization;


namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [AllowAnonymous]
    public class ItemController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ItemController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("searchItems")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchItems(
            [FromQuery] bool FilterOn = false,
            [FromQuery] ItemType? itemType = null,
            [FromQuery] string? title = null,   
            [FromQuery] bool? isAvailable = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] string? brand = null,
            [FromQuery] ItemBrandId? itemBrandId = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool descending = false)
        {
            try
            {
                IQueryable<Item> itemsQuery = _dbContext.Items;
                    

             if (FilterOn)
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
                        item.TitleImageUrl,
                    })
                    .ToListAsync();
                if (items.Count == 0)
                    {
                        return NotFound("No items found for the specified criteria");
                    }
                    

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



                [HttpGet("getImage/{guid}")]
                [AllowAnonymous]
        public async Task<IActionResult> GetImage(Guid guid, [FromQuery] int index = -1)
        {
            try
            {
                var item = await _dbContext.Items.FirstOrDefaultAsync(i => i.GUID == guid);

                if (item == null)
                {
                    return NotFound($"Item with GUID {guid} not found");
                }

                if (index == -1)
                {
                    // Get the title image
                    if (string.IsNullOrEmpty(item.TitleImageUrl))
                    {
                        return NotFound("Image not found for item");
                    }

                    if (!Uri.TryCreate(item.TitleImageUrl, UriKind.Absolute, out _))
                    {
                        return BadRequest("Invalid title image URL");
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
                            return NotFound("Error fetching title image for item");
                        }
                        catch (Exception)
                        {
                            return StatusCode(500, "Error fetching title image for item");
                        }
                    }
                }
                else
                {
                    // Get additional image by index
                    if (item.AdditionalImageUrls == null || item.AdditionalImageUrls.Count == 0)
                    {
                        return NotFound($"No additional images found for item with GUID {guid}");
                    }

                    if (index < 0 || index >= item.AdditionalImageUrls.Count)
                    {
                        return BadRequest("Invalid index parameter");
                    }

                    var imageUrl = item.AdditionalImageUrls[index];

                    if (!Uri.TryCreate(imageUrl, UriKind.Absolute, out _))
                    {
                        return BadRequest($"Invalid additional image URL at index {index}");
                    }

                    using (var httpClient = new HttpClient())
                    {
                        try
                        {
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
                            // Handle invalid URL exception
                            return NotFound($"Error fetching additional image for item with GUID {guid} at index {index}");
                        }
                    }
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("getItem/{guid}")]
        [AllowAnonymous]
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
        [Authorize(Policy = "EmployeeOrAdmin")]
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

                var itemDetails = editItemDTO.ItemDetails;

                // Update common properties
                itemToUpdate.Title = itemDetails.Title ?? itemToUpdate.Title;
                itemToUpdate.Description = itemDetails.Description ?? itemToUpdate.Description;
                itemToUpdate.Quantity = itemDetails.Quantity > 0 ? itemDetails.Quantity : itemToUpdate.Quantity;
                itemToUpdate.Price = itemDetails.Price > 0 ? itemDetails.Price : itemToUpdate.Price;
                itemToUpdate.Brand = itemDetails.Brand ?? itemToUpdate.Brand;
                itemToUpdate.ItemBrandId = editItemDTO.ItemDetails.ItemBrandId; // Update ItemBrandId enum
                itemToUpdate.IsAvailable = itemDetails.IsAvailable;
                itemToUpdate.TitleImageUrl = itemDetails.TitleImageUrl ?? itemToUpdate.TitleImageUrl;
                itemToUpdate.AdditionalImageUrls = itemDetails.AdditionalImageUrls ?? itemToUpdate.AdditionalImageUrls;

                // Handle Film-specific properties
                if (itemToUpdate is Film filmToUpdate && editItemDTO.FilmDetails != null)
                {
                    filmToUpdate.FilmColorState = editItemDTO.FilmDetails.FilmColorState;
                    filmToUpdate.FilmFormat = editItemDTO.FilmDetails.FilmFormat;
                    filmToUpdate.FilmISO = editItemDTO.FilmDetails.FilmISO;
                    filmToUpdate.FilmExposure = editItemDTO.FilmDetails.FilmExposure;
                }

                // Handle Camera-specific properties
                if (itemToUpdate is Camera cameraToUpdate && editItemDTO.CameraDetails != null)
                {
                    cameraToUpdate.CameraFocalLength = editItemDTO.CameraDetails.CameraFocalLength;
                    cameraToUpdate.CameraMaxShutterSpeed = editItemDTO.CameraDetails.CameraMaxShutterSpeed;
                    cameraToUpdate.CameraMegapixel = editItemDTO.CameraDetails.CameraMegapixel;
                    cameraToUpdate.CameraFilmFormat = editItemDTO.CameraDetails.CameraFilmFormat;
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
        [Authorize(Policy = "EmployeeOrAdmin")]
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
                                    ItemType = cameraItemDetails.ItemType = ItemType.Camera,
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

        [HttpDelete("deleteImage")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult DeleteImage(
            [FromQuery] Guid? guid = null,
            [FromQuery] int? index = null)
        {
            try
            {
                // Validate that both guid and index are provided
                if (!guid.HasValue || !index.HasValue)
                {
                    return BadRequest("Both GUID and index must be provided");
                }

                IQueryable<Item> itemsToDelete = _dbContext.Items;

                // Apply filters based on query parameters
                if (guid.HasValue)
                {
                    itemsToDelete = itemsToDelete.Where(item => item.GUID == guid.Value);
                }

                // Execute the query to get the items to delete
                var items = itemsToDelete.ToList();

                if (items.Count == 0)
                {
                    return NotFound("No items found for the specified criteria");
                }

                foreach (var item in items)
                {
                    if (index.HasValue && index != -1)
                    {
                        // Soft delete additional image by writing "deletedimage" to the URL
                        if (item.AdditionalImageUrls != null && index >= 0 && index < item.AdditionalImageUrls.Count)
                        {
                            item.AdditionalImageUrls[index.Value] = "deletedimage";
                        }
                    }
                    else
                    {
                        // Soft delete the title image by writing "deletedimage" to the URL
                        if (!string.IsNullOrEmpty(item.TitleImageUrl))
                        {
                            item.TitleImageUrl = "deletedimage";
                        }
                    }
                }

                // Save changes to the database
                _dbContext.SaveChanges();

                return Ok("Images soft deleted successfully");
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("deleteItem/{guid}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult DeleteItem(Guid guid)
        {
            try
            {
                var itemToDelete = _dbContext.Items.FirstOrDefault(item => item.GUID == guid);

                if (itemToDelete == null)
                {
                    return NotFound($"Item with GUID {guid} not found");
                }

                _dbContext.Items.Remove(itemToDelete);
                _dbContext.SaveChanges();

                return Ok($"Item with GUID {guid} deleted successfully");
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(500, "Internal Server Error");
            }
        }


[HttpDelete("deleteAllItems")]
[Authorize(Policy = "AdminOnly")]

public IActionResult DeleteAllItems()
{
    try
    {
        var itemsToDelete = _dbContext.Items.ToList();

        if (itemsToDelete.Count == 0)
        {
            return NotFound("No items found to delete");
        }

        _dbContext.Items.RemoveRange(itemsToDelete);
        _dbContext.SaveChanges();

        return Ok("All items deleted successfully");
    }
    catch (Exception)
    {
        // Log the exception
        return StatusCode(500, "Internal Server Error");
    }
}


    }
}





