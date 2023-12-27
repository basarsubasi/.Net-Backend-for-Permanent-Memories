using Microsoft.AspNetCore.Identity;
namespace WebApplication1.Models.UserRelatedModels
{
public class SeedData
{
    public static async Task InitializeRolesAndAdminUser(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        // Create roles if they don't exist
        string[] roleNames = { "Admin", "Customer", "Employee" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                // Create the role and seed it to the database
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Create an admin user if it doesn't exist
        var adminUser = await userManager.FindByEmailAsync("admin@example.com");

        if (adminUser == null)
        {
            // Create a new ApplicationUser
            var newAdminUser = new ApplicationUser
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                // Add other properties as needed
            };

            // Create the user with the password and assign the "Admin" role
            var createAdminUserResult = await userManager.CreateAsync(newAdminUser, "Admin123!"); // Change the password as needed

            if (createAdminUserResult.Succeeded)
            {
                await userManager.AddToRoleAsync(newAdminUser, "Admin");
            }
        }
    }
}
}
