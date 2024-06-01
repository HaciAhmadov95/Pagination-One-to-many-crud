using Fiorella.Data;
using Fiorella.ViewModels.Baskets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Fiorella.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _contextAccessor;
        public CartController(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _appDbContext = context;
            _contextAccessor = contextAccessor;
        }















        [HttpGet]

        public async Task<IActionResult> Index()
        {


            List<BasketVM> basketProducts = null;

            if (_contextAccessor.HttpContext.Request.Cookies["basket"] is not null)
            {
                basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_contextAccessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basketProducts = new List<BasketVM>();
            }




            var products = await _appDbContext.Products.Include(m => m.Category)
                                                      .Include(m => m.ProductImages)
                                                      .ToListAsync();

            List<BasketProductsVM> basket = new();

            foreach (var item in basketProducts)
            {
                var dbProduct = products.FirstOrDefault(m => m.Id == item.Id);
                basket.Add(new BasketProductsVM
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Description = dbProduct.Description,
                    Price = dbProduct.Price,
                    Image = dbProduct.ProductImages.FirstOrDefault(m => m.IsMain).Name,
                    Category = dbProduct.Category.Name,
                    Count = item.Count
                });
            }


            CartVM response = new()
            {
                BasketProducts = basket,
                Total = basketProducts.Sum(m => m.Count * m.Price)
            };

            return View(response);
        }












        //[HttpPut]
        //public async Task<IActionResult> Increase(int? id)
        //{
        //    if (id is null) return BadRequest();

        //    List<BasketVM> basketProducts = null;

        //    if (_contextAccessor.HttpContext.Request.Cookies["basket"] is not null)
        //    {
        //        basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_contextAccessor.HttpContext.Request.Cookies["basket"]);
        //    }

        //    BasketVM dbBsket = basketProducts.FirstOrDefault(m => m.Id == id);

        //    dbBsket.Count++;

        //    _contextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProducts));

        //    int count = basketProducts.Sum(m => m.Count);
        //    int dbBasketCount = dbBsket.Count;
        //    decimal total = basketProducts.Sum(m => m.Count * m.Price);

        //    return Ok(new { count, total, dbBasketCount });
        //}

        //[HttpPut]
        //public async Task<IActionResult> Decrease(int? id)
        //{
        //    if (id is null) return BadRequest();

        //    List<BasketVM> basketProducts = null;

        //    if (_contextAccessor.HttpContext.Request.Cookies["basket"] is not null)
        //    {
        //        basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_contextAccessor.HttpContext.Request.Cookies["basket"]);
        //    }

        //    BasketVM dbBsket = basketProducts.FirstOrDefault(m => m.Id == id);

        //    if (dbBsket.Count > 0)
        //    {
        //        dbBsket.Count--;
        //    }

        //    _contextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProducts));

        //    int count = basketProducts.Sum(m => m.Count);
        //    int dbBasketCount = dbBsket.Count;
        //    decimal total = basketProducts.Sum(m => m.Count * m.Price);

        //    return Ok(new { count, total, dbBasketCount });
        //}







        [HttpPost]
        public IActionResult DeleteProductFromBasket(int? id)
        {


            List<BasketVM> basketProducts = new();

            if (_contextAccessor.HttpContext.Request.Cookies["basket"] is not null)
            {
                basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_contextAccessor.HttpContext.Request.Cookies["basket"]);
            }


            basketProducts = basketProducts.Where(m => m.Id != id).ToList();

            _contextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProducts));

            int count = basketProducts.Sum(m => m.Count);
            decimal total = basketProducts.Sum(m => m.Count * m.Price);

            return Ok(new { count, total });


        }
    }
}
