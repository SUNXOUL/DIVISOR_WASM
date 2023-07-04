
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DIVISOR_WASM.Server.Services.TeamService;
using Shared.Models;
using Shared;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService TeamServices;
        public TeamController(ITeamService TeamService)
        {
            this.TeamServices = TeamService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Team>>>> GetAllTeames()
        {
            var productos = await TeamServices.GetAllTeamsAsync();
            return Ok(productos);
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<ServiceResponse<Team>>> GetTeam(int ID)
        {
            var result = await TeamServices.GetTeamAsync(ID);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Team>>> Save(Team Team)
        {
            var result = await TeamServices.Save(Team);
            return Ok(result);
        }

        [HttpPatch]
        public async Task<ActionResult<ServiceResponse<Boolean>>> Shuffle()
        {
            var result = await TeamServices.Shuffle();
            return Ok(result);
        }

        [HttpDelete("{TeamId}")]
        public async Task<ActionResult<ServiceResponse<Team>>> Delete(int TeamId)
        {
            var result = await TeamServices.Delete(TeamId);
            return Ok(result);
        }
    }
}
