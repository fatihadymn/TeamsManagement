using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TeamsManagement.Api;
using TeamsManagement.Core.Services;
using TeamsManagement.Data;
using TeamsManagement.Items.Mappers;
using TeamsManagement.Items.Models.Requests;

namespace TeamsManagement.Test
{
    public class PlayerServiceTests
    {
        private readonly DbContextOptions<ApplicationContext> _contextOptions;
        private readonly IMapper _mapper;

        public PlayerServiceTests()
        {
            _contextOptions = new DbContextOptionsBuilder<ApplicationContext>()
           .UseInMemoryDatabase("PlayerServiceTests")
           .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
           .Options;

            using var context = new ApplicationContext(_contextOptions);

            Initializer.PrepareTestDatas(context);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TeamProfile>();
                cfg.AddProfile<PlayerProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task PlayerService_GetAllPlayers()
        {
            using var context = new ApplicationContext(_contextOptions);
            var service = new PlayerService(context, _mapper);

            var players = await service.GetAllPlayersAsync();

            Assert.NotNull(players);
            Assert.NotEmpty(players);
            Assert.Equal(2, players.Count);
        }

        [Fact]
        public async Task PlayerService_GetSinglePlayer()
        {
            using var context = new ApplicationContext(_contextOptions);
            var service = new PlayerService(context, _mapper);

            var players = await service.GetAllPlayersAsync();

            var player = await service.GetSinglePlayerAsync(new(players[0].Id));

            Assert.NotNull(player);
            Assert.NotEmpty(players);
            Assert.Equal(players[0].Id, player.Id);
            Assert.Equal(players[0].Name, player.Name);
        }

        [Fact]
        public async Task PlayerService_CreatePlayer()
        {
            using var context = new ApplicationContext(_contextOptions);
            var service = new PlayerService(context, _mapper);

            var player = await service.CreatePlayerAsync(new("Created Test Player", 180, DateTime.Parse("09.09.1985"), null));

            Assert.NotNull(player);
            Assert.NotEqual(Guid.Empty, player.Id);
        }

        [Fact]
        public async Task PlayerService_UpdatePlayer()
        {
            using var context = new ApplicationContext(_contextOptions);
            var service = new PlayerService(context, _mapper);

            var players = await service.GetAllPlayersAsync();

            await service.UpdatePlayerAsync(
                new UpdatePlayerRequest
                {
                    Id = players[0].Id,
                    Name = "Updated Test Player",
                    DateOfBirth = DateTime.Parse("08.08.1990"),
                    Height = 190
                });

            var player = await service.GetSinglePlayerAsync(new(players[0].Id));

            Assert.NotNull(player);
            Assert.NotEqual(Guid.Empty, player.Id);
            Assert.Equal("Updated Test Player", player.Name);
        }

        [Fact]
        public async Task PlayerService_UpdatePlayerTeam()
        {
            using var context = new ApplicationContext(_contextOptions);
            var service = new PlayerService(context, _mapper);

            var players = await service.GetAllPlayersAsync();

            await service.UpdatePlayerTeamAsync(
                new UpdatePlayerTeamRequest()
                {
                    PlayerId = players[0].Id,
                    TeamId = null
                });

            var player = await service.GetSinglePlayerAsync(new(players[0].Id));

            Assert.NotNull(player);
            Assert.Null(player.TeamId);
            Assert.Null(player.TeamName);
        }
    }
}
