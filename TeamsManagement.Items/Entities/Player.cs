namespace TeamsManagement.Items.Entities
{
    public class Player : BaseEntity
    {
        public required string Name { get; set; }

        public required int Height { get; set; }

        public required DateTime DateOfBirth { get; set; }

        public Guid? TeamId { get; set; }

        public Team? Team { get; set; }
    }
}
