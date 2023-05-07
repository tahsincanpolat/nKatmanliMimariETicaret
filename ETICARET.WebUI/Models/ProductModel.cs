using ETICARET.Entities;
using System.ComponentModel.DataAnnotations;

namespace ETICARET.WebUI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required] // Zorunlu alan
        [StringLength(60, MinimumLength = 10, ErrorMessage = "Ürün ismi min 10 max 60 karakter olmalıdır.")]  // Name alanı karakter sınırlaması
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Ürünün açıklaması min 10 max 100 karakter olmalıdır.")]
        public string Description { get; set; }
        public List<Image> Images { get; set; }
        [Required]
        [Range(10000,40000)] // Aralık belirtme
        public decimal Price { get; set; }  

        public List<Category> SelectedCategory { get; set; }

        public ProductModel()
        {
            Images = new List<Image>();
        }
    }
}
