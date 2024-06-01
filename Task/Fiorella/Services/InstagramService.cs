using Fiorella.Data;
using Fiorella.Models;
using Fiorella.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Services
{
    public class InstagramService : IInstagramService
    {
        private readonly AppDbContext _context;

        public InstagramService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Instagram>> GetAllAsync()
        {
            return await _context.Instagram.ToListAsync();
        }
    }
}
