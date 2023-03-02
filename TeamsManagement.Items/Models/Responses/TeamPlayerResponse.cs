namespace TeamsManagement.Items.Models.Responses
{
    public class TeamPlayerResponse
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public int Age { get; set; }
    }
}
