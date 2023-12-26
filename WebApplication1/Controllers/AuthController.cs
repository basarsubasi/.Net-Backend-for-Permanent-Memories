
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.UserRelatedModels;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Route("api/auth")]
    [ApiController]
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

}
}
