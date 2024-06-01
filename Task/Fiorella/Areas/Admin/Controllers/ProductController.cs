using Fiorella.Data;
using Fiorella.Helpers.Extensions;
using Fiorella.Models;
using Fiorella.Services.Interface;
using Fiorella.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace Fiorella.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;
        public ProductController(AppDbContext context, IProductService productService, ICategoryService categoryService, IWebHostEnvironment env)
        {
            _context = context;
            _productService = productService;
            _categoryService = categoryService;
            _env = env;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var paginateDatas = await _productService.GetAllPaginateAsync(page);
            var mappedDatas = _productService.GetMappedDatas(paginateDatas);

            ViewBag.pageCount = await GetPageCountAsync(4);
            ViewBag.currentPage = page;

            return View(mappedDatas);
        }

        public async Task<int> GetPageCountAsync(int take)
        {
            int count = await _productService.GetCountAsync();
            return (int)Math.Ceiling((decimal)count / take);
        }



        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await _productService.GetByIdAsync((int)id);

            if (product is null) return NotFound();


            List<ProductImageVM> productImages = new();

            foreach (var item in product.ProductImages)
            {
                productImages.Add(new ProductImageVM
                {
                    Image = item.Name,
                    IsMain = item.IsMain
                });
            }

            ProductDetailVM model = new()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category.Name,
                Images = productImages
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.categories = await _categoryService.GetAllBySelectedAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM request)
        {
            ViewBag.categories = await _categoryService.GetAllBySelectedAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in request.Images)
            {
                if (!item.CheckFileSize(500))
                {
                    ModelState.AddModelError("Images", "Image size must be max 500 kb");
                    return View();
                }

                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Images", "File must be only image");
                    return View();
                }
            }

            List<ProductImage> images = new();


            foreach (var item in request.Images)
            {
                string fileName = Guid.NewGuid().ToString() + " " + item.FileName;

                string path = Path.Combine(_env.WebRootPath, "img", fileName);

                await item.SavaFileToLocalAsync(path);

                images.Add(new ProductImage
                {
                    Name = fileName
                });

            }

            images.FirstOrDefault().IsMain = true;


            Product product = new()
            {
                Name = request.Name,
                Description = request.Description,
                Price = decimal.Parse(request.Price.Replace(".", ",")),
                CategoryId = request.CategoryId,
                ProductImages = images
            };

            await _productService.CreateAsync(product);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Product product = await _productService.GetByIdAsync((int)id);

            if (product == null)
            {
                return NotFound();
            }

            foreach (var item in product.ProductImages)
            {
                string path = Path.Combine(_env.EnvironmentName, "img", item.Name);

                path.DeleteFileFromLocal();
            }
            await _productService.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Product product = await _productService.GetByIdAsync((int)id);

            if (product == null)
            {
                return NotFound();
            }

            List<ProductImageVM> imageVM = new();

            ViewBag.categories = await _categoryService.GetAllBySelectedAsync();

            foreach (var item in product.ProductImages)
            {
                imageVM.Add(new ProductImageVM
                {
                    Image = item.Name,
                    IsMain = item.IsMain
                });
            }
            return View(new ProductEditVM { Name = product.Name, Category = product.Category.Name, Description = product.Description, Price = product.Price, Images = imageVM });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id, ProductEditVM productEditVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.categories = await _categoryService.GetAllBySelectedAsync();
                return View();
            }

            Product existProduct = await _productService.GetByIdAsync((int)id);

            if (existProduct == null) return NotFound();

            existProduct.Name = productEditVM.Name;
            existProduct.Description = productEditVM.Description;
            existProduct.Price = productEditVM.Price;
            existProduct.CategoryId = productEditVM.CategoryId;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
