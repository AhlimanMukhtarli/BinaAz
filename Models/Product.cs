using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Drawing2D;

namespace BinaAz.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        


        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price invalid")]
        public decimal Price { get; set; }

        [Required]
        public int Area { get; set; }
        public byte RoomCount { get; set; } 

        [StringLength(1000)]
        
        public string Description { get; set; }

        public bool IsActive { get; set; }


       
        public List<image> Images { get; set; } 
        public List<ProductAttribute> Attributes { get; set; }
        

    }
}
