namespace Fiorella.Models
{
    public class ProductImage:BaseEntity
    {
        public string Name { get; set; }
        public bool IsMain { get; set; }=false;
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
