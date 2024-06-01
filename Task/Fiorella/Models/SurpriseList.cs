namespace Fiorella.Models
{
    public class SurpriseList : BaseEntity
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public int SurpriseId { get; set; }
        public Surprise Surprise { get; set; }
    }
}
