using System.Text.Json.Serialization;

namespace TeamsManagement.Items.Models.Requests
{
    public class UpdatePlayerTeamRequest
    {
        [JsonIgnore]
        public Guid PlayerId { get; set; }

        public Guid? TeamId { get; set; }
    }
}
