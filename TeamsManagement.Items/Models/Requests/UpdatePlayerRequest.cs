using System.Text.Json.Serialization;

namespace TeamsManagement.Items.Models.Requests
{
    public class UpdatePlayerRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public int Height { get; set; }

        public DateTime DateOfBirth { get; set; }

    }
}
