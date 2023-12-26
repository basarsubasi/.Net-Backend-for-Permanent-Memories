
using Microsoft.AspNetCore.Identity;


namespace WebApplication1.Models.UserRelatedModels
{
    public class ApplicationUser : IdentityUser
    {
     public bool IsCustomer { get; set; }
     public bool IsEmployee { get; set; }
    }
}
