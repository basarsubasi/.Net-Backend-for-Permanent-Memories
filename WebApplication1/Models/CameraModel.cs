using System;
using WebApplication1.Data;
using WebApplication1.Enums.CameraEnums;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication1.Models
{
    

    public class Camera:Item
    {
       
        public CameraFocalLength CameraFocalLength { get; set; }
        public CameraMaxShutterSpeed CameraMaxShutterSpeed { get; set; }
        public CameraMegapixel CameraMegapixel { get; set; }
        public CameraFilmFormat CameraFilmFormat { get; set; }
    }
}