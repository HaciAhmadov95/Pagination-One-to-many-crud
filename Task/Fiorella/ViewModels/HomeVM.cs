using Fiorella.Models;

namespace Fiorella.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public SliderInfo SliderInfo { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public List<Surprise> Surprises { get; set; }
        public List<SurpriseList> SurpriseLists { get; set; }
        public List<Expert> Experts { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<ExpertSlide> ExpertSlides { get; set; }
        public List<Instagram> Instagrams { get; set; }

    }
}
