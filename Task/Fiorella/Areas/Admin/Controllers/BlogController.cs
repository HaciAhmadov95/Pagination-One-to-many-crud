using Fiorella.Data;
using Fiorella.Models;
using Fiorella.ViewModels.Blogs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        //GetAll

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            List<Blog> categories = await _context.Blogs.OrderByDescending(m => m.Id)
                                                                 .ToListAsync();

            List<BlogVM> model = categories.Select(m => new BlogVM { Id = m.Id, Title = m.Title, Description = m.Description, Date = m.Date, Image = m.Image })
                                               .ToList();
            return View(model);
        }
        //Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogVM blog)
        {
            if (!ModelState.IsValid)
            {
                return View(blog);
            }

            bool existBlog = await _context.Blogs.AnyAsync(m => m.Title.Trim() == blog.Title.Trim());

            if (existBlog)
            {
                ModelState.AddModelError("Name", "This title already exist");
                return View();
            }

            await _context.Blogs.AddAsync(new Blog { Title = blog.Title, Description = blog.Description, Date = blog.Date, Image = blog.Image });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //Detail

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Blog blog = await _context.Blogs.FindAsync(id);



            if (blog == null)
            {
                return NotFound();
            }

            BlogDetailVM model = new()
            {
                Title = blog.Title,
                Description = blog.Description,
                Date = blog.Date,
                Image = blog.Image,

            };


            return View(model);
        }

        //Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Blog blog = await _context.Blogs.FindAsync(id);


            if (blog == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Edit

        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Blog blog = await _context.Blogs.Where(m => m.Id == id)
                                                         .FirstOrDefaultAsync();


            if (blog == null)
            {
                return NotFound();
            }

            return View(new BlogEditVM { Id = blog.Id, Title = blog.Title, Description = blog.Description, Date = blog.Date, Image = blog.Image });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id, BlogEditVM blogEditVM)
        {
            if (id == null)
            {
                return BadRequest();
            }


            Blog dbBlog = await _context.Blogs.Where(m => m.Id == id)
                                                         .FirstOrDefaultAsync();


            if (dbBlog == null)
            {
                return NotFound();
            }

            dbBlog.Title = blogEditVM.Title;
            dbBlog.Description = blogEditVM.Description;
            dbBlog.Date = blogEditVM.Date;
            dbBlog.Image = blogEditVM.Image;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }





    }
}

