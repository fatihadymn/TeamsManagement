using TeamsManagement.Items.Models.Requests;
using TeamsManagement.Items.Models.Responses;

namespace TeamsManagement.Core.Services
{
    public interface IPlayerService : IServiceBase
    {
        Task<List<GetAllPlayersResponse>> GetAllPlayersAsync();
        Task<GetSinglePlayerResponse> GetSinglePlayerAsync(GetSinglePlayerRequest request);
        Task<CreatePlayerResponse> CreatePlayerAsync(CreatePlayerRequest request);
        Task UpdatePlayerAsync(UpdatePlayerRequest request);
        Task UpdatePlayerTeamAsync(UpdatePlayerTeamRequest request);
    }
}
