
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.UserRelatedModels;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

       [HttpPost("register/customer")]
       [AllowAnonymous]
public async Task<IActionResult> RegisterCustomer([FromBody] RegistrationModel model)
{
    return await RegisterUser(model, "Customer");
}

[HttpPost("register/employee")]
public async Task<IActionResult> RegisterEmployee([FromBody] RegistrationModel model)
{
    return await RegisterUser(model, "Employee");
}

private async Task<IActionResult> RegisterUser(RegistrationModel model, string role)
{
    if (string.IsNullOrEmpty(model.Email) || !IsValidEmail(model.Email))
    {
        return BadRequest("Invalid email format");
    }

    if (ModelState.IsValid)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

        // Set IsCustomer or IsEmployee based on the role
        user.IsCustomer = role == "Customer";
        user.IsEmployee = role == "Employee";

        if (model.Password != null)
        {
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
                return Ok(new { Message = "Registration successful" });
            }

            return BadRequest(result.Errors);
        }

        return BadRequest("Password cannot be null");
    }

    return BadRequest(ModelState);
}

private bool IsValidEmail(string email)
{
    // Use a simple regex for email validation
    // This regex is for basic email format validation and may not cover all cases
    const string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
    return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
}


[HttpPost("login")]
[AllowAnonymous]
public async Task<IActionResult> Login([FromBody] LoginModel model)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
    {
        return BadRequest("Email or password cannot be null or empty");
    }

    var user = await _userManager.FindByEmailAsync(model.Email);

    if (user == null)
    {
        return BadRequest("User not found");
    }

    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: false);

    if (result.Succeeded)
    {
        var roles = await _userManager.GetRolesAsync(user);
        return Ok(new
        {
            Message = "Login successful",
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Roles = roles
        });
    }

    if (result.IsLockedOut)
    {
        return BadRequest("Account is locked out");
    }

    if (result.IsNotAllowed)
    {
        return BadRequest("User is not allowed to sign in");
    }

    if (result.RequiresTwoFactor)
    {
        // Handle two-factor authentication if needed
        return BadRequest("Two-factor authentication is required");
    }

    // Password is incorrect
    if (result == Microsoft.AspNetCore.Identity.SignInResult.Failed)
    {
        return BadRequest("Invalid password");
    }

    return BadRequest("Invalid login attempt");
}


[HttpPost("logout")]
[Authorize]
public async Task<IActionResult> Logout()
{
    await _signInManager.SignOutAsync();
    return Ok(new { Message = "Logout successful" });
}


[HttpDelete("deleteUsers/{userId}")]
[Authorize(Policy = "AdminOnly")]
public async Task<IActionResult> DeleteUser(string userId)
{
    if (Guid.TryParse(userId, out Guid userGuid))
    {
        var user = await _userManager.FindByIdAsync(userGuid.ToString());

        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User deleted successfully" });
            }

            return BadRequest(result.Errors);
        }

        return BadRequest("User not found");
    }

    return BadRequest("Invalid user ID format");
}

// Modify the existing action in your AuthController
[HttpGet("GetUsers")]
[Authorize(Policy = "AdminOnly")] // Make sure only authorized users can list users (adjust role as needed)
public IActionResult ListUsers([FromQuery] string role)
{
    IQueryable<ApplicationUser> usersQuery;

    if (!string.IsNullOrEmpty(role))
    {
        // Filter users based on the specified role
        usersQuery = _userManager.GetUsersInRoleAsync(role).Result.AsQueryable();
    }
    else
    {
        // If no role is specified, retrieve all users
        usersQuery = _userManager.Users;
    }

    var users = usersQuery.Select(user => new
    {
        UserId = user.Id,
        UserName = user.UserName,
        Email = user.Email
        // Add any other user properties you want to include in the response
    });

    return Ok(users);
}

 [HttpGet("GetCustomers")]
 [Authorize(Policy = "EmployeeOrAdmin")]
 
    public IActionResult GetCustomers()
    {
        // Retrieve users with the "Customer" role
        var customers = _userManager.GetUsersInRoleAsync("Customer").Result;

        // You may want to project the user information or perform additional processing here
        var customerList = customers.Select(user => new
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email
            // Add other properties as needed
        }).ToList();

        return Ok(customerList);
    }
}





}

