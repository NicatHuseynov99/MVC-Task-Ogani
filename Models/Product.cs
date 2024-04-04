using System.ComponentModel.DataAnnotations.Schema;

namespace Ogani.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }

        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public bool InStock { get; set; } 
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
