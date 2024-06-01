using Fiorella.Data;
using Fiorella.Models;
using Fiorella.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
