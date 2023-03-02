namespace TeamsManagement.Items.Models.Responses
{
    public class GetAllTeamPlayersResponse
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Country { get; set; }

        public List<TeamPlayerResponse> Players { get; set; } = new List<TeamPlayerResponse>();
    }
}
