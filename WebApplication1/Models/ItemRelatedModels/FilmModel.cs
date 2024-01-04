

using WebApplication1.Enums.FilmEnums;



namespace WebApplication1.Models.ItemRelatedModels
{
    

    public class Film:Item
    {
        // Film characteristics
        public bool IsBlackAndWhiteFilm()
    {
        if (FilmColorState == FilmColorState.BlackAndWhite)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
      

        // Film brand information


        // Custom unique identifier based on film characteristics
        

        // Enums for Film characteristics
        public FilmColorState FilmColorState { get; set; }
        public FilmFormat FilmFormat { get; set; }
        public FilmISO FilmISO { get; set; }
        public FilmExposure FilmExposure { get; set; }

        // Constructor to initialize some default values
 }
        
}

