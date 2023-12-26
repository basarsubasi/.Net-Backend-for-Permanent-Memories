using WebApplication1.Enums.FilmEnums;
namespace WebApplication1.DTOs
{
    public class FilmDTO
    {
        public FilmColorState FilmColorState { get; set; }
        public FilmFormat FilmFormat { get; set; }
        public FilmISO FilmISO { get; set; }
        public FilmExposure FilmExposure { get; set; }
        // Exclude TitleImage and other properties you want to ignore
    }
}

    
