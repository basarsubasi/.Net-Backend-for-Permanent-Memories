using WebApplication1.Enums.CameraEnums;

namespace WebApplication1.DTOs
{
    public class CameraDTO
    {
        public CameraFocalLength CameraFocalLength { get; set; }
        public CameraMaxShutterSpeed CameraMaxShutterSpeed { get; set; }
        public CameraFilmFormat CameraFilmFormat { get; set; }
    }
}