using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models.ItemRelatedModels;
using WebApplication1.Enums.FilmEnums;
using WebApplication1.Enums.ItemEnums;
using System;


namespace WebApplication1.Data
{
    public class FilmConfig : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {

            // Basic film information
            builder.Property(f => f.Title).IsRequired();
            builder.Property(f => f.Description).IsRequired();
            builder.Property(f => f.Quantity).IsRequired();

           

            // Price and availability
    
            builder.Property(f => f.Price).IsRequired().HasColumnType("decimal(18, 2)");
            builder.Property(f => f.Brand).IsRequired();
            builder.Property(f => f.IsAvailable).IsRequired();
            builder.Property(f => f.ItemType).IsRequired();
            builder.Property(f => f.TitleImageUrl).IsRequired();
            builder.Property(f => f.AdditionalImageUrls).IsRequired();

             // Film characteristics
        



            // Enums for Film characteristics
            builder.Property(f => f.FilmColorState).IsRequired();
            builder.Property(f => f.FilmFormat).IsRequired();
            builder.Property(f => f.FilmISO).IsRequired();
            builder.Property(f => f.FilmExposure).IsRequired();
            builder.HasData(
                new Film
                {
                    Title = "Kodak Portra 400",
                    Description = "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.",
                    Quantity = 400,
                    Price = 10.99m,
                    Brand = "Kodak",
                    ItemType = ItemType.Film,
                    IsAvailable = true,
                    TitleImageUrl = "https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg",
                    AdditionalImageUrls = new List<string>
                    {
                        "https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg",
                        "https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg",
                        "https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg"
                    },
                    FilmColorState = FilmColorState.BlackAndWhite,
                    FilmFormat = FilmFormat._35mm,
                    FilmISO = FilmISO.ISO400,
                    FilmExposure = FilmExposure._36
                }
            );
        }

    }
}
