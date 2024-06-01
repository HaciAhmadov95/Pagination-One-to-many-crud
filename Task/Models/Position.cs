namespace Fiorella.Models
{
    public class Position : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Expert> Experts { get; set; }
    }
}
