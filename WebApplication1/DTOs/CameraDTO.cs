using WebApplication1.Enums;

namespace WebApplication1.DTOs
{
    public class CameraDTO
    {
        public CameraFocalLength CameraFocalLength { get; set; }
        public CameraMaxShutterSpeed CameraMaxShutterSpeed { get; set; }
        public CameraMegapixel CameraMegapixel { get; set; }
        public CameraFilmFormat CameraFilmFormat { get; set; }
    }
}