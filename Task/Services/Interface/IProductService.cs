using Fiorella.Models;

namespace Fiorella.Services.Interface
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
    }
}
