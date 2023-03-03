using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TeamsManagement.Core.Services;
using TeamsManagement.Items.Exceptions;
using TeamsManagement.Items.Models.Requests;
using TeamsManagement.Items.Models.Responses;

namespace TeamsManagement.Api.Controllers
{
    [Route("api/team-management/v1/players")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        /// <summary>Get all players.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetAllPlayersResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPlayers()
        {
            return Ok(await _playerService.GetAllPlayersAsync());
        }

        /// <summary>
        /// Get single player.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(GetSinglePlayerResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlayer([FromRoute] Guid id)
        {
            return Ok(await _playerService.GetSinglePlayerAsync(new(id)));
        }

        /// <summary>
        /// Update single player.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePlayer([FromRoute] Guid id, [FromBody] UpdatePlayerRequest request)
        {
            request.Id = id;

            await _playerService.UpdatePlayerAsync(request);

            return Ok();
        }

        /// <summary>
        /// Create a new player to a team.
        /// </summary>
        /// /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreatePlayerResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerRequest request)
        {
            return StatusCode(StatusCodes.Status201Created, await _playerService.CreatePlayerAsync(request));
        }

        /// <summary>
        /// Remove a player from a team or assign another team.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePlayerTeam([FromRoute] Guid id, [FromBody] UpdatePlayerTeamRequest request)
        {
            request.PlayerId = id;

            await _playerService.UpdatePlayerTeamAsync(request);

            return Ok();
        }
    }
}
