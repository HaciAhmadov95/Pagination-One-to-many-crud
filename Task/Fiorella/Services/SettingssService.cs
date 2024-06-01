using Fiorella.Data;
using Fiorella.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Services
{
    public class SettingssService : ISettingssService
    {
        private readonly AppDbContext _context;
        public SettingssService(AppDbContext context)
        {
            _context = context;
        }



        public async Task<Dictionary<string, string>> GetAllAsync()
        {

            return await _context.Settingsses.ToDictionaryAsync(m => m.Key, m => m.Value);

        }
    }
}
