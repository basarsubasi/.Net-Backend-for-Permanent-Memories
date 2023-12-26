
   // LoginModel.cs
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.UserRelatedModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}

