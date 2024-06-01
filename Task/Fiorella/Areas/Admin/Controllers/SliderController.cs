using Fiorella.Data;
using Fiorella.Helpers.Extensions;
using Fiorella.Models;
using Fiorella.ViewModels.Sliders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();

            List<SliderVM> result = sliders.Select(m => new SliderVM { Id = m.Id, Image = m.Image }).ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            foreach (var item in request.Images)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Images", "File must be onlu image format");
                    return View();
                }

                if (!item.CheckFileSize(200))
                {
                    ModelState.AddModelError("Images", "Size is not acceptable");
                    return View();
                }
            }


            foreach (var item in request.Images)
            {
                string imageName = Guid.NewGuid().ToString() + "-" + item.FileName;

                string path = Path.Combine(_env.WebRootPath, "img", imageName);

                await item.SavaFileToLocalAsync(path);

                await _context.Sliders.AddAsync(new Slider { Image = imageName });

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (id is null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "img", slider.Image);

            path.DeleteFileFromLocal();

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var slider = _context.Sliders.FirstOrDefault(m => m.Id == id);

            if (id is null) return NotFound();

            return View(new SliderEditVM { Image = slider.Image });
        }


        [HttpPost]

        public async Task<IActionResult> Edit(int? id, SliderEditVM request)
        {
            if (id is null) return BadRequest();

            var slider = _context.Sliders.FirstOrDefault(m => m.Id == id);

            if (id is null) return NotFound();


            if (request.NewImage is null) return RedirectToAction(nameof(Index));

            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "File must be onlu image format");
                return View();
            }

            if (!request.NewImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "Size is not acceptable");
                request.Image = slider.Image;
                return View(request);
            }

            string oldPath = Path.Combine(_env.WebRootPath, "img", slider.Image);

            oldPath.DeleteFileFromLocal();

            string imageName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;

            string newPath = Path.Combine(_env.WebRootPath, "img", imageName);

            await request.NewImage.SavaFileToLocalAsync(newPath);

            slider.Image = imageName;

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }


        [HttpGet]

        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var sliders = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (id == null) return NotFound();

            return View(new SliderDetailVM { Image = sliders.Image });
        }
    }
}
