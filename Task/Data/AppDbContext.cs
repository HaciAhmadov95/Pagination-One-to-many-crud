
using Fiorella.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderInfo> SlidersInfo { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Surprise> Surprise { get; set; }
        public DbSet<SurpriseList> SurpriseLists { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<ExpertSlide> ExpertSlides { get; set; }
        public DbSet<Instagram> Instagram { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Blog>()
        //                .HasData(
        //        new Blog
        //        {
        //            Id = 1,
        //            Title = "Blog1",
        //            Description = "Desc1",
        //            Date = DateTime.Now,
        //            Image = "blog-feature-image-1.jpg"
        //        },
        //         new Blog
        //         {
        //             Id = 2,
        //             Title = "Blog2",
        //             Description = "Desc2",
        //             Date = DateTime.Now,
        //             Image = "blog-feature-image-3.jpg"
        //         },
        //          new Blog
        //          {
        //              Id = 3,
        //              Title = "Blog3",
        //              Description = "Desc3",
        //              Date = DateTime.Now,
        //              Image = "blog-feature-image-4.jpg"
        //          }
        //        );
        //}


    }
}
