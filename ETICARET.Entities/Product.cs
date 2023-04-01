namespace ETICARET.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Image> Images { get; set; }
        public decimal Price { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<Comment> Comments { get; set; }

        public Product()
        {
            Images = new List<Image>();
            ProductCategories = new List<ProductCategory> ();
            Comments = new List<Comment> ();
        }
    }
}