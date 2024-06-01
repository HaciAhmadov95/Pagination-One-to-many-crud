using Fiorella.Models;
using Fiorella.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fiorella.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<List<CategoryVM>> GetAllOrderByDescendingAsync();
        Task<bool> ExistAsync(string name);
        Task CreateAsync(CategoryCreateVM category);
        Task<Category> GetWithProductAsync(int id);
        Task DeleteAsync(Category category);
        Task<Category> GetByIdAsync(int id);
        Task EditAsync(Category category, CategoryEditVM categoryEdit);
        Task<SelectList> GetAllBySelectedAsync();

    }
}
