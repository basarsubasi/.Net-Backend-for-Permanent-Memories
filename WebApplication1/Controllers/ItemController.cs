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

    public class ItemController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ItemController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        [HttpPost("createItem/{itemType}")]
        [Authorize(Policy = "AdminOnly")]
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
                                    IsAvailable = cameraItemDetails.IsAvailable,
                                    ItemType = cameraItemDetails.ItemType = ItemType.Camera,
                                    TitleImageUrl = cameraItemDetails.TitleImageUrl ?? "Default Title Image URL",
                                    AdditionalImageUrls = cameraItemDetails.AdditionalImageUrls ?? new List<string>(),
                                    CameraFocalLength = cameraDetails.CameraFocalLength,
                                    CameraMaxShutterSpeed = cameraDetails.CameraMaxShutterSpeed,
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




        [HttpPost("reduceStockAfterPurchase/{guid}")]
        [AllowAnonymous]
public IActionResult PurchaseItem(Guid guid, [FromBody] int quantityToPurchase)
{
    try
    {
        var itemToPurchase = _dbContext.Items.FirstOrDefault(item => item.GUID == guid);

        if (itemToPurchase == null)
        {
            return NotFound($"Item with GUID {guid} not found");
        }

        if (quantityToPurchase <= 0)
        {
            return BadRequest("Quantity to purchase must be greater than zero");
        }

        if (itemToPurchase.Quantity < quantityToPurchase)
        {
            return BadRequest("Insufficient item quantity available");
        }

        // Reduce the item's quantity
        itemToPurchase.Quantity -= quantityToPurchase;

        // Check if the item's quantity is now zero and update its availability
        if (itemToPurchase.Quantity == 0)
        {
            itemToPurchase.IsAvailable = false;
        }

        _dbContext.SaveChanges();

        return Ok($"Purchase successful. Remaining quantity of item: {itemToPurchase.Quantity}");
    }
    catch (Exception ex)
    {
        // Log the exception (add appropriate logging here)
        return StatusCode(500, $"Internal Server Error: {ex.Message}");
    }
}

        [HttpGet("searchItems")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchItems(
            [FromQuery] ItemType? itemType = null,
            [FromQuery] string? title = null,   
            [FromQuery] bool? isAvailable = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] string? brand = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool descending = false)
        {
            try
            {
                IQueryable<Item> itemsQuery = _dbContext.Items;

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

    }
}




