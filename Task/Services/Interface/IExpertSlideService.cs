using Fiorella.Models;

namespace Fiorella.Services.Interface
{
    public interface IExpertSlideService
    {
        Task<List<ExpertSlide>> GetAllAsync();
    }
}
