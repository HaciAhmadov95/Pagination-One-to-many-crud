using Fiorella.Data;
using Fiorella.Models;
using Fiorella.Services.Interface;
using Fiorella.ViewModels;
using Fiorella.ViewModels.Baskets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Fiorella.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IExpertSlideService _expertSlideService;
        private readonly IInstagramService _instagramService;
        private readonly IHttpContextAccessor _accessor;
        public HomeController(AppDbContext context,
                             IProductService productService,
                             ICategoryService categoryService,
                             IExpertSlideService expertSlideService,
                             IInstagramService instagramService,
                             IHttpContextAccessor accessor)
        {
            _context = context;
            _productService = productService;
            _categoryService = categoryService;
            _expertSlideService = expertSlideService;
            _instagramService = instagramService;
            _accessor = accessor;
        }


        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _categoryService.GetCategoriesAsync();
            List<Product> products = await _productService.GetAllWithImagesAsync();
            List<Surprise> surprises = await _context.Surprise.ToListAsync();
            List<SurpriseList> surpriseLists = await _context.SurpriseLists.ToListAsync();
            List<Expert> experts = await _context.Experts.Include(m => m.Positions).ToListAsync();
            List<Blog> blogs = await _context.Blogs.ToListAsync();
            List<ExpertSlide> expertSlides = await _expertSlideService.GetAllAsync();
            List<Instagram> instagrams = await _instagramService.GetAllAsync();


            HomeVM model = new()
            {
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










        [HttpPost]

        public async Task<IActionResult> AddProductToBasket(int? id)
        {

            if (id is null) return BadRequest();

            List<BasketVM> basketProducts = null;

            if (_accessor.HttpContext.Request.Cookies["basket"] is not null)
            {
                basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basketProducts = new List<BasketVM>();
            }



            var dbProduct = await _context.Products.Include(m => m.Category).Include(m => m.ProductImages).FirstOrDefaultAsync(m => m.Id == (int)id);

            var existProduct = basketProducts.FirstOrDefault(m => m.Id == (int)id);

            if (existProduct is not null)
            {
                existProduct.Count++;
            }
            else
            {
                basketProducts.Add(new BasketVM
                {
                    Id = (int)id,
                    Count = 1,
                    Price = dbProduct.Price,
                    Name = dbProduct.Name,
                    Description = dbProduct.Description,
                    Category = dbProduct.Category.Name,
                    Image = dbProduct.ProductImages.FirstOrDefault(m => m.IsMain && !m.SoftDeleted)?.Name


                });
            }

            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProducts));


            int count = basketProducts.Sum(m => m.Count);
            decimal total = basketProducts.Sum(m => m.Count * m.Price);

            return Ok(new { count, total });
        }
    }
}