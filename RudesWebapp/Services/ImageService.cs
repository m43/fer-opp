using System.Linq;
using RudesWebapp.Models;

namespace RudesWebapp.Services
{
    public class ImageService
    {
        private static readonly int[] SupportedSizes = {72, 240, 480, 640, 960, 1280};

        public static int SanitizeSize(int value)
        {
            return value >= 1280 ? 1280 : SupportedSizes.First(size => size >= value);
        }
        
        public const string ImagesFolder = "images";
        
    }
}