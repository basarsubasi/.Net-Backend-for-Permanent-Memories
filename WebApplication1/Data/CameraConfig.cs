using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Enums;

namespace WebApplication1.Data
{
    public class CameraConfig : IEntityTypeConfiguration<Camera>
    {
        public void Configure(EntityTypeBuilder<Camera> builder)
        {
            // Basic camera information
            builder.Property(c => c.Title).IsRequired();
            builder.Property(c => c.Description).IsRequired();
            builder.Property(c => c.Quantity).IsRequired();

            // Price and availability
            builder.Property(c => c.Price).IsRequired();
            builder.Property(c => c.Brand).IsRequired();
            builder.Property(c => c.IsAvailable).IsRequired();
            builder.Property(c => c.ItemType).IsRequired();
            builder.Property(c => c.TitleImageUrl).IsRequired();
            builder.Property(c => c.AdditionalImageUrls).IsRequired();

            // Camera characteristics
            builder.Property(c => c.CameraFocalLength).IsRequired();
            builder.Property(c => c.CameraMaxShutterSpeed).IsRequired();
            builder.Property(c => c.CameraMegapixel).IsRequired();

            // Seed initial camera data
            builder.HasData(
                new Camera
                {
                    Title = "Canon EOS Rebel T7",
                    Description = "A versatile and affordable entry-level DSLR camera.",
                    Quantity = 100,
                    Price = 499.99m,
                    Brand = "Canon",
                    IsAvailable = true,
                    ItemType = ItemType.Camera,
                    TitleImageUrl = "https://example.com/canon_eos_rebel_t7.jpg",
                    AdditionalImageUrls = new List<string>
                    {
                        "https://example.com/canon_eos_rebel_t7_1.jpg",
                        "https://example.com/canon_eos_rebel_t7_2.jpg"
                    },
                    CameraFocalLength = CameraFocalLength._50mm,
                    CameraMaxShutterSpeed = CameraMaxShutterSpeed.ShutterSpeed3,
                    CameraMegapixel =   CameraMegapixel.Megapixel_20
                }
            );
        }
    }
}
