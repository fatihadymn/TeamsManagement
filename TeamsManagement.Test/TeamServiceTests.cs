using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;
using TeamsManagement.Core.Services;
using TeamsManagement.Data;
using TeamsManagement.Items.Entities;
using TeamsManagement.Items.Mappers;

namespace TeamsManagement.Test
{
    public class TeamServiceTests
    {
        private readonly DbContextOptions<ApplicationContext> _contextOptions;
        private readonly IMapper _mapper;
        private readonly ILogger<TeamService> _logger;

        public TeamServiceTests()
        {
            _contextOptions = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase("TeamServiceTests")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

            using var context = new ApplicationContext(_contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.AddRange(
                new Team
                {
                    Country = "Spain",
                    Name = "Barcelona",
                    CreatedOn = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Players = new List<Player>()
                    {
                        new Player { Name = "test1", DateOfBirth = DateTime.Parse("09.09.1985"), Height=178,Id = Guid.NewGuid(), CreatedOn=DateTime.Now }
                    }
                },
                new Team
                {
                    Country = "France",
                    Name = "Paris SG",
                    CreatedOn = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Players = new List<Player>()
                    {
                        new Player { Name = "test2", DateOfBirth = DateTime.Parse("09.09.1990"), Height=180,Id = Guid.NewGuid(), CreatedOn=DateTime.Now }
                    }
                });

            context.SaveChanges();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TeamProfile>();
                cfg.AddProfile<PlayerProfile>();
            });
            _mapper = config.CreateMapper();

            var mock = new Mock<ILogger<TeamService>>();
            _logger = mock.Object;
        }

        [Fact]
        public async Task TeamService_GetAllTeams()
        {
            using var context = new ApplicationContext(_contextOptions);
            var service = new TeamService(context, _logger, _mapper);

            var teams = await service.GetAllTeamsAsync();

            Assert.NotNull(teams);
            Assert.NotEmpty(teams);
            Assert.Equal(2, teams.Count);
        }

        [Fact]
        public async Task TeamService_GetAllTeamPlayers()
        {
            using var context = new ApplicationContext(_contextOptions);
            var service = new TeamService(context, _logger, _mapper);

            var teams = await service.GetAllTeamsAsync();
            var teamPlayers = await service.GetAllTeamPlayersAsync(new(teams.FirstOrDefault()!.Id));

            Assert.NotNull(teamPlayers);
            Assert.NotEmpty(teamPlayers.Players);
            Assert.Single(teamPlayers.Players);
        }
    }
}