using System.ComponentModel.DataAnnotations;

namespace BinaAz.Models
{
    public class CategoryAttribute
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category ID is required")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
