using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.ItemRelatedModels;



namespace WebApplication1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add configurations for all entity types
            modelBuilder.ApplyConfiguration(new FilmConfig()); // Add this line
            modelBuilder.ApplyConfiguration(new CameraConfig());
            
            // Add configurations for other entity types if needed
        }
    }
}
