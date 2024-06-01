namespace Fiorella.Services.Interface
{
    public interface ISettingssService
    {
        Task<Dictionary<string, string>> GetAllAsync();
    }
}
