using Fiorella.Data;
using Fiorella.Models;
using Fiorella.Services.Interface;
using Fiorella.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IExpertSlideService _expertSlideService;
        private readonly IInstagramService _instagramService;
        public HomeController(AppDbContext context,
                             IProductService productService,
                             ICategoryService categoryService,
                             IExpertSlideService expertSlideService,
                             IInstagramService instagramService)
        {
            _context = context;
            _productService = productService;
            _categoryService = categoryService;
            _expertSlideService = expertSlideService;
            _instagramService = instagramService;
        }


        public async Task<IActionResult> Index()
        {
            List<Slider> slider = await _context.Sliders.ToListAsync();
            SliderInfo sliderInfo = await _context.SlidersInfo.FirstOrDefaultAsync();
            List<Category> categories = await _categoryService.GetCategoriesAsync();
            List<Product> products = await _productService.GetAllAsync();
            List<Surprise> surprises = await _context.Surprise.ToListAsync();
            List<SurpriseList> surpriseLists = await _context.SurpriseLists.ToListAsync();
            List<Expert> experts = await _context.Experts.Include(m => m.Positions).ToListAsync();
            List<Blog> blogs = await _context.Blogs.ToListAsync();
            List<ExpertSlide> expertSlides = await _expertSlideService.GetAllAsync();
            List<Instagram> instagrams = await _instagramService.GetAllAsync();


            HomeVM model = new()
            {
                Sliders = slider,
                SliderInfo = sliderInfo,
                Categories = categories,
                Products = products,
                Surprises = surprises,
                SurpriseLists = surpriseLists,
                Experts = experts,
                Blogs = blogs,
                ExpertSlides = expertSlides,
                Instagrams = instagrams


            };

            return View(model);
        }
    }
}