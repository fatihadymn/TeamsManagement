namespace TeamsManagement.Items.Models.Requests
{
    public record CreatePlayerRequest(string Name, int Height, DateTime DateOfBirth, Guid? TeamId);
}
