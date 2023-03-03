using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TeamsManagement.Core.Services;
using TeamsManagement.Items.Exceptions;
using TeamsManagement.Items.Models.Responses;

namespace TeamsManagement.Api.Controllers
{
    [Route("api/team_management/v1/teams")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        /// <summary>Get all teams.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetAllTeamsResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeams()
        {
            return Ok(await _teamService.GetAllTeamsAsync());
        }

        /// <summary>Get all player in a team.</summary>
        /// <param name="id"></param>
        [HttpGet("{id:guid}/players")]
        [ProducesResponseType(typeof(List<GetAllTeamPlayersResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeam([FromRoute] Guid id)
        {
            return Ok(await _teamService.GetAllTeamPlayersAsync(new(id)));
        }
    }
}
