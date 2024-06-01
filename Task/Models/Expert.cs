namespace Fiorella.Models
{
    public class Expert : BaseEntity
    {
        public string FullName { get; set; }
        public string Image { get; set; }
        public int PositionId { get; set; }
        public Position Positions { get; set; }

    }
}
