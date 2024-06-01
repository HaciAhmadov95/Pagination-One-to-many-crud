using Fiorella.Data;
using Fiorella.Models;
using Fiorella.Services.Interface;
using Fiorella.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Fiorella.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICategoryService _categoryService;
        public CategoryController(AppDbContext context, ICategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {


            return View(await _categoryService.GetAllOrderByDescendingAsync());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            bool existCategory = await _categoryService.ExistAsync(category.Name);
            if (existCategory)
            {
                ModelState.AddModelError("Name", "This already Exist");

                return View();
            }

            await _categoryService.CreateAsync(category);

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Category category = await _categoryService.GetWithProductAsync((int)id);



            if (category == null)
            {
                return NotFound();
            }

            CategoryDetailVM model = new()
            {
                Name = category.Name,
                ProductCount = category.Products.Count()
            };


            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Category category = await _categoryService.GetWithProductAsync((int)id);


            if (category == null)
            {
                return NotFound();
            }

            await _categoryService.DeleteAsync(category);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Category category = await _categoryService.GetByIdAsync((int)id);


            if (category == null)
            {
                return NotFound();
            }

            return View(new CategoryEditVM { Id = category.Id, Name = category.Name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id, CategoryEditVM categoryEditVM)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Category dbCategory = await _categoryService.GetByIdAsync((int)id);




            if (dbCategory == null)
            {
                return NotFound();
            }

            await _categoryService.EditAsync(dbCategory, categoryEditVM);

            return RedirectToAction(nameof(Index));
        }
    }
}
