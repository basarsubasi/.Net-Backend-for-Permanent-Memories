
using WebApplication1.Enums.CameraEnums;



namespace WebApplication1.Models.ItemRelatedModels
{
    

    public class Camera:Item
    {
       
        public CameraFocalLength CameraFocalLength { get; set; }
        public CameraMaxShutterSpeed CameraMaxShutterSpeed { get; set; }
        public CameraFilmFormat CameraFilmFormat { get; set; }
    }
}