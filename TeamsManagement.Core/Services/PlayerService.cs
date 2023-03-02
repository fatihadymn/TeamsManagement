﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeamsManagement.Data;
using TeamsManagement.Items.Entities;
using TeamsManagement.Items.Exceptions;
using TeamsManagement.Items.Models.Requests;
using TeamsManagement.Items.Models.Responses;

namespace TeamsManagement.Core.Services
{
    public class PlayerService : ServiceBase, IPlayerService
    {
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<PlayerService> _logger;
        private readonly IMapper _mapper;

        public PlayerService(ApplicationContext dbContext, ILogger<PlayerService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<GetAllPlayersResponse>> GetAllPlayersAsync()
        {
            return _mapper.Map<List<GetAllPlayersResponse>>(await _dbContext.Players.ToListAsync());
        }

        public async Task<GetSinglePlayerResponse> GetSinglePlayerAsync(GetSinglePlayerRequest request)
        {
            var result = await _dbContext.Players.Where(x => x.Id == request.PlayerId).Include(x => x.Team).FirstOrDefaultAsync();

            if (result is null)
            {
                _logger.LogError($"Player not found with Id:{request.PlayerId}");

                throw new BusinessException("Player not found");
            }

            return _mapper.Map<GetSinglePlayerResponse>(result);
        }

        public async Task<CreatePlayerResponse> CreatePlayerAsync(CreatePlayerRequest request)
        {
            if (request.TeamId.HasValue)
            {
                var isTeamExist = await _dbContext.Teams.AnyAsync(x => x.Id == request.TeamId.Value);

                if (!isTeamExist)
                {
                    _logger.LogError("Team does not exist to create player");
                    throw new BusinessException("Team does not exist to create player");
                }
            }

            var player = _mapper.Map<Player>(request);

            await _dbContext.Players.AddAsync(player);

            await _dbContext.SaveChangesAsync();

            return new CreatePlayerResponse()
            {
                Id = player.Id,
            };
        }

        public async Task UpdatePlayerAsync(UpdatePlayerRequest request)
        {
            var player = await _dbContext.Players.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (player is null)
            {
                _logger.LogError("Player does not exist to update player");
                throw new BusinessException("Player does not exist to update player");
            }

            player.Name = request.Name;
            player.Height = request.Height;
            player.DateOfBirth = request.DateOfBirth;

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePlayerTeamAsync(UpdatePlayerTeamRequest request)
        {
            if (request.TeamId.HasValue)
            {
                var isTeamExist = await _dbContext.Teams.AnyAsync(x => x.Id == request.TeamId.Value);

                if (!isTeamExist)
                {
                    _logger.LogError("Team does not exist to update player team");
                    throw new BusinessException("Team does not exist to update player team");
                }
            }

            var player = await _dbContext.Players.FirstOrDefaultAsync(x => x.Id == request.PlayerId);

            if (player is null)
            {
                _logger.LogError("Player does not exist to update player team");
                throw new BusinessException("Player does not exist to update player team");
            }

            player.TeamId = request.TeamId;

            await _dbContext.SaveChangesAsync();
        }
    }
}
