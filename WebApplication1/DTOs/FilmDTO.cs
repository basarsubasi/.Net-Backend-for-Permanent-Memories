using WebApplication1.Enums;
namespace WebApplication1.DTOs
{
    public class FilmDTO
    {
        public WebApplication1.Enums.FilmColorState FilmColorState { get; set; }
        public WebApplication1.Enums.FilmSize FilmSize { get; set; }
        public WebApplication1.Enums.FilmISO FilmISO { get; set; }
        public WebApplication1.Enums.FilmExposure FilmExposure { get; set; }
        // Exclude TitleImage and other properties you want to ignore
    }
}

    
