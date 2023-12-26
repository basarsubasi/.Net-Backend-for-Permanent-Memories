// IdentityDbContext.cs
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.UserRelatedModels;

public class IdentityAppDbContext : IdentityDbContext<ApplicationUser>
{
    public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options)
        : base(options)
    {
    }

    // You don't need to add DbSet properties for IdentityUser, IdentityRole, etc.
    // They are already included in IdentityDbContext
}
