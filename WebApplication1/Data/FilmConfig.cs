using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

using WebApplication1.Enums;

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
            builder.Property(f => f.Price).IsRequired();
            builder.Property(f => f.Brand).IsRequired();
            builder.Property(f => f.IsAvailable).IsRequired();

             // Film characteristics
        



            // Enums for Film characteristics
            builder.Property(f => f.FilmColorState).IsRequired();
            builder.Property(f => f.FilmSize).IsRequired();
            builder.Property(f => f.FilmISO).IsRequired();
            builder.Property(f => f.FilmExposure).IsRequired();
            builder.HasData(
                new Film
                {
                    Title = "Kodak Portra 400",
                    Description = "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.",
                    Quantity = 400,
                    Price = 10.99m,
                    Brand = "Ilford",
                    IsAvailable = true,
                    FilmColorState = FilmColorState.BlackAndWhite,
                    FilmSize = FilmSize._35mm,
                    FilmISO = FilmISO.ISO400,
                    FilmExposure = FilmExposure._36
                }
            );
        }

    }
}
