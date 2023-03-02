namespace TeamsManagement.Items.Models.Responses
{
    public class GetAllPlayersResponse
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public int Age { get; set; }
    }
}
