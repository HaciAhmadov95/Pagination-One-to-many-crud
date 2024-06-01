using Fiorella.Models;

namespace Fiorella.Services.Interface
{
    public interface IInstagramService
    {
        Task<List<Instagram>> GetAllAsync();
    }
}
