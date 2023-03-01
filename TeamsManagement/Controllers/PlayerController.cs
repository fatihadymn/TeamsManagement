using Microsoft.AspNetCore.Mvc;

namespace TeamsManagement.Controllers
{
    [Route("api/team_management/v1/players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        [HttpGet]
        //[ProducesResponseType(typeof(List<DailyRateDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPlayers()
        {
            return Ok();
        }
    }
}
