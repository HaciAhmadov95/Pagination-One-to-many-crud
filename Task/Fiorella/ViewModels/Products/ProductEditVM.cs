namespace Fiorella.ViewModels.Products
{
    public class ProductEditVM
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public List<IFormFile> Photos { get; set; }
        public List<ProductImageVM> Images { get; set; }
    }
}
