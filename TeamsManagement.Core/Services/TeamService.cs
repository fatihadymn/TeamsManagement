using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeamsManagement.Data;
using TeamsManagement.Items.Exceptions;
using TeamsManagement.Items.Models.Requests;
using TeamsManagement.Items.Models.Responses;

namespace TeamsManagement.Core.Services
{
    public class TeamService : ServiceBase, ITeamService
    {
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<TeamService> _logger;
        private readonly IMapper _mapper;

        public TeamService(ApplicationContext dbContext, ILogger<TeamService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<GetAllTeamsResponse>> GetAllTeamsAsync()
        {
            return _mapper.Map<List<GetAllTeamsResponse>>(await _dbContext.Teams.ToListAsync());
        }

        public async Task<GetAllTeamPlayersResponse> GetAllTeamPlayersAsync(GetAllTeamPlayersRequest request)
        {
            var result = await _dbContext.Teams.Where(x => x.Id == request.TeamId).Include(x => x.Players).FirstOrDefaultAsync();

            if (result == null)
            {
                _logger.LogError($"Team not found with Id:{request.TeamId}");

                throw new BusinessException("Team not found");
            }

            return _mapper.Map<GetAllTeamPlayersResponse>(result);
        }
    }
}
