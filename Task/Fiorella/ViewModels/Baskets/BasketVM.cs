namespace Fiorella.ViewModels.Baskets
{
    public class BasketVM
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
    }
}
