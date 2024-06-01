using Fiorella.Data;
using Fiorella.Models;
using Fiorella.Services.Interface;
using Fiorella.ViewModels.Products;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Include(m => m.Category)
                                          .Include(m => m.ProductImages)
                                          .ToListAsync();
        }

        public async Task<List<Product>> GetAllPaginateAsync(int page, int take = 4)
        {
            return await _context.Products.Include(m => m.Category)
                                          .Include(m => m.ProductImages)
                                          .Skip((page - 1) * take)
                                          .Take(take)
                                          .ToListAsync();
        }

        public async Task<List<Product>> GetAllWithImagesAsync()
        {
            return await _context.Products.Include(m => m.ProductImages).Include(m => m.Category).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                                                     .Include(m => m.Category)
                                                     .Include(m => m.ProductImages)
                                                     .Where(m => !m.SoftDeleted)
                                                     .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public List<ProductVM> GetMappedDatas(List<Product> products)
        {
            return products.Select(m => new ProductVM
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                Image = m.ProductImages.FirstOrDefault(m => m.IsMain).Name,
                Category = m.Category.Name
            }).ToList();
        }

        public async Task CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
