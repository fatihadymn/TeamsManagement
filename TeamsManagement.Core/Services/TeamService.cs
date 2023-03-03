using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeamsManagement.Data;
using TeamsManagement.Items.Exceptions;
using TeamsManagement.Items.Models.Requests;
using TeamsManagement.Items.Models.Responses;

namespace TeamsManagement.Core.Services
{
    public class TeamService : ServiceBase, ITeamService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public TeamService(ApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<GetAllTeamsResponse>> GetAllTeamsAsync()
        {
            return _mapper.Map<List<GetAllTeamsResponse>>(await _dbContext.Teams.ToListAsync());
        }

        public async Task<GetAllTeamPlayersResponse> GetAllTeamPlayersAsync(GetAllTeamPlayersRequest request)
        {
            var result = await _dbContext.Teams.Where(x => x.Id == request.TeamId).Include(x => x.Players).FirstOrDefaultAsync();

            if (result is null)
            {
                throw new BusinessException($"Team not found with Id:{request.TeamId}");
            }

            return _mapper.Map<GetAllTeamPlayersResponse>(result);
        }
    }
}
