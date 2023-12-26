using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Enums.CameraEnums;
using WebApplication1.Enums.ItemEnums;

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
            builder.Property(c => c.ItemBrandId).IsRequired();
            builder.Property(c => c.IsAvailable).IsRequired();
            builder.Property(c => c.ItemType).IsRequired();
            builder.Property(c => c.TitleImageUrl).IsRequired();
            builder.Property(c => c.AdditionalImageUrls).IsRequired();

            // Camera characteristics
            builder.Property(c => c.CameraFocalLength).IsRequired();
            builder.Property(c => c.CameraMaxShutterSpeed).IsRequired();
            builder.Property(c => c.CameraMegapixel).IsRequired();
            builder.Property(c => c.CameraFilmFormat).IsRequired();

            // Seed initial camera data
            builder.HasData(
                new Camera
                {
                    Title = "Canon EOS Rebel T7",
                    Description = "A versatile and affordable entry-level DSLR camera.",
                    Quantity = 100,
                    Price = 499.99m,
                    Brand = "Canon",
                    ItemBrandId = ItemBrandId.canon,
                    IsAvailable = true,
                    ItemType = ItemType.Camera,
                    TitleImageUrl = "https://example.com/canon_eos_rebel_t7.jpg",
                    AdditionalImageUrls = new List<string>
                    {
                        "https://example.com/canon_eos_rebel_t7_1.jpg",
                        "https://example.com/canon_eos_rebel_t7_2.jpg"
                    },
                    CameraFocalLength = CameraFocalLength._50mm,
                    CameraMaxShutterSpeed = CameraMaxShutterSpeed.ShutterSpeed1_1000,
                    CameraMegapixel = CameraMegapixel.Megapixel_20,
                    CameraFilmFormat = CameraFilmFormat._35mm
                }
            );
        }
    }
}
