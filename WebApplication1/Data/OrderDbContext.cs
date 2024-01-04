using Microsoft.EntityFrameworkCore;
using WebApplication1.Models; // Replace with your actual namespace


namespace WebApplication1.Data

{
public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<OrderItem>()
        .HasKey(oi => oi.OriginalItemGUID);
        
        base.OnModelCreating(modelBuilder);
        // Define relationships and any configuration here
        // For example:
        modelBuilder.Entity<Order>()
            .HasMany(o => o.Items)
            .WithOne() // Assuming no navigation property back to Order in OrderItem
            .HasForeignKey("OrderId"); // Replace with the actual foreign key property name if different
    }
}
}