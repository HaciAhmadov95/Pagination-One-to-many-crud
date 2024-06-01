using Fiorella.Data;
using Fiorella.Models;
using Fiorella.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Services
{
    public class ExpertSlideService : IExpertSlideService
    {
        private readonly AppDbContext _context;

        public ExpertSlideService(AppDbContext context)
        {
            _context = context;
        }



        public async Task<List<ExpertSlide>> GetAllAsync()
        {
            return await _context.ExpertSlides.ToListAsync();
        }
    }
}
