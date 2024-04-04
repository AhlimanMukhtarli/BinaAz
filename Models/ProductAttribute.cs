using System.ComponentModel.DataAnnotations;

namespace BinaAz.Models
{
    public class ProductAttribute
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string Value { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsActive { get; set; }
    }
}
