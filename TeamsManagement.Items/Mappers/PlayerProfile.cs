using AutoMapper;
using TeamsManagement.Items.Entities;
using TeamsManagement.Items.Models.Requests;
using TeamsManagement.Items.Models.Responses;

namespace TeamsManagement.Items.Mappers
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player, TeamPlayerResponse>().AfterMap((src, dest) =>
            {
                dest.Age = Convert.ToInt32((DateTime.Now - src.DateOfBirth).TotalDays) / 365;
            });

            CreateMap<Player, GetSinglePlayerResponse>().AfterMap((src, dest) =>
            {
                dest.Age = Convert.ToInt32((DateTime.Now - src.DateOfBirth).TotalDays) / 365;
                dest.TeamName = src.Team?.Name;
                dest.TeamId = src.Team?.Id;
            });

            CreateMap<Player, CreatePlayerResponse>();

            CreateMap<CreatePlayerRequest, Player>().AfterMap((src, dest) =>
            {
                dest.Id = Guid.NewGuid();
                dest.CreatedOn = DateTime.Now;
                dest.DateOfBirth = src.DateOfBirth.Date;
            });

            CreateMap<Player, GetAllPlayersResponse>().AfterMap((src, dest) =>
            {
                dest.Age = Convert.ToInt32((DateTime.Now - src.DateOfBirth).TotalDays) / 365;
            });
        }
    }
}
