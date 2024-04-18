using BinaAz.Models;
namespace BinaAz.Models.ViewModels
{
    public class ImageViewModel
    {
        public int Id { get; set; }
        public List<image> Images { get; set; }
        public IFormFile ImgFile { get; set; }
    }
}