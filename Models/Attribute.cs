using System.ComponentModel.DataAnnotations;

namespace BinaAz.Models
{
    public class Attribute
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Value is required")]
        public string Value { get; set; }

        [Required(ErrorMessage = "CategoryAttribute ID is required")]
        public int CategoryAttributeID { get; set; }

        public CategoryAttribute CategoryAttribute { get; set; }

    }
}
