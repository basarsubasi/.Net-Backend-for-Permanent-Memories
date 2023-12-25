// Models/Film.cs
using System;
using WebApplication1.Data;
using WebApplication1.Enums;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication1.Models
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
        public FilmSize FilmSize { get; set; }
        public FilmISO FilmISO { get; set; }
        public FilmExposure FilmExposure { get; set; }

        // Constructor to initialize some default values
 }
        
}

