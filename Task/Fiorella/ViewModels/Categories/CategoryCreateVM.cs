using System.ComponentModel.DataAnnotations;

namespace Fiorella.ViewModels.Categories
{
    public class CategoryCreateVM
    {

        [Required(ErrorMessage = "This field can't be empty")]
        [StringLength(20, ErrorMessage = "Max length must be 20")]
        public string Name { get; set; }
    }
}
