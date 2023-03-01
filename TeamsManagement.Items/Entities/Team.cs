namespace TeamsManagement.Items.Entities
{
    public class Team : BaseEntity
    {
        public required string Name { get; set; }

        public required string Country { get; set; }

        public List<Player> Players { get; set; } = new List<Player>();
    }
}
