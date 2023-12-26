using System.Threading.Tasks;
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
public async Task<IActionResult> Login([FromBody] LoginModel model)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    if (model.Email == null || model.Password == null)
    {
        return BadRequest("Email or password cannot be null");
    }

    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);

    if (result.Succeeded)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
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

        return BadRequest("User not found");
    }

    if (result.IsLockedOut)
    {
        return BadRequest("Account is locked out");
    }

    return BadRequest("Invalid login attempt");
}


        // Additional actions and methods can be added as needed
    }
}
