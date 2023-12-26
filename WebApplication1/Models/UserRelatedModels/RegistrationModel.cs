
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.UserRelatedModels
{     
   public class RegistrationModel
{
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "Password and ConfirmPassword must match")]
    public string? ConfirmPassword { get; set; }
}

}



