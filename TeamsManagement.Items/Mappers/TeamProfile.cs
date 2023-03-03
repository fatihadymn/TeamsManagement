using AutoMapper;
using TeamsManagement.Items.Entities;
using TeamsManagement.Items.Models.Responses;

namespace TeamsManagement.Items.Mappers
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, GetAllTeamsResponse>();

            CreateMap<Team, GetAllTeamPlayersResponse>();
        }
    }
}
