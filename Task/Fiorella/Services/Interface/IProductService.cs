using Fiorella.Models;
using Fiorella.ViewModels.Products;

namespace Fiorella.Services.Interface
{
    public interface IProductService
    {
        Task<List<Product>> GetAllWithImagesAsync();
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        List<ProductVM> GetMappedDatas(List<Product> products);
        Task<List<Product>> GetAllPaginateAsync(int page, int take = 4);
        Task<int> GetCountAsync();
        Task CreateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}
