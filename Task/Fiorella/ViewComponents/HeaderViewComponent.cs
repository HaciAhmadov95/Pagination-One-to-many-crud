using Fiorella.Services.Interface;
using Fiorella.ViewModels;
using Fiorella.ViewModels.Baskets;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Fiorella.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingssService _settingssService;
        private readonly IHttpContextAccessor _accessor;
        public HeaderViewComponent(ISettingssService settingssService,
                                   IHttpContextAccessor accessor)
        {
            _settingssService = settingssService;
            _accessor = accessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Dictionary<string, string> settingDatas = await _settingssService.GetAllAsync();


            List<BasketVM> basketProducts = new();

            if (_accessor.HttpContext.Request.Cookies["basket"] is not null)
            {
                basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }


            HeaderVM response = new()
            {
                Settings = settingDatas,
                BasketCount = basketProducts.Sum(m => m.Count),
                BasketTotalPrice = basketProducts.Sum(m => m.Count * m.Price),
            };

            return await Task.FromResult(View(response));
        }
    }
}
