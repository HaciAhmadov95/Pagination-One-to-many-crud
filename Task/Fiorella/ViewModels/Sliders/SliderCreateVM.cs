using System.ComponentModel.DataAnnotations;

namespace Fiorella.ViewModels.Sliders
{
    public class SliderCreateVM
    {
        [Required]
        public List<IFormFile> Images { get; set; }
    }
}
