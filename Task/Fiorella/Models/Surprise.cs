namespace Fiorella.Models
{
    public class Surprise : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public string Image { get; set; }
        public ICollection<SurpriseList> SurpriseList { get; set; }
    }
}
