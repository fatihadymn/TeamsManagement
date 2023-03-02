using TeamsManagement.Items.Models.Requests;
using TeamsManagement.Items.Models.Responses;

namespace TeamsManagement.Core.Services
{
    public interface ITeamService : IServiceBase
    {
        Task<List<GetAllTeamsResponse>> GetAllTeamsAsync();
        Task<GetAllTeamPlayersResponse> GetAllTeamPlayersAsync(GetAllTeamPlayersRequest request);
    }
}
