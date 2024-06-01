using System.ComponentModel.DataAnnotations;

namespace Fiorella.ViewModels.Blogs
{
    public class BlogCreateVM
    {

        [Required(ErrorMessage = "This field can't be empty")]
        [StringLength(20, ErrorMessage = "Max length must be 20")]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
    }
}
