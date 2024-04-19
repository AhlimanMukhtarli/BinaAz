using BinaAz.Models;

namespace BinaAz.Models.ViewModels
{
    public class AttributeViewModel
    {
        public int ProductId { get; set; }
        public List<ProductAttribute> ProductAttributes { get; set; }
        public ProductAttribute productAttribute { get; set; }
    }
}