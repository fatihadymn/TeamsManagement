namespace TeamsManagement.Items.Models.Responses
{
    public class GetSinglePlayerResponse
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public int Height { get; set; }

        public int Age { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? TeamName { get; set; }

        public Guid? TeamId { get; set; }
    }
}
