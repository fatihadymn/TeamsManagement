namespace TeamsManagement.Items.Models.Responses
{
    public class GetAllTeamsResponse
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Country { get; set; }
    }
}
