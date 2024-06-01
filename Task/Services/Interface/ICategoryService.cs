using Fiorella.Models;

namespace Fiorella.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoriesAsync();
    }
}
