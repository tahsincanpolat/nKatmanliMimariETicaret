using ETICARET.Entities;

namespace ETICARET.WebUI.Models
{
    public class ProductDetailsModel
    {
        public Product Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Comment> Comments { get; set; }
    }

}
