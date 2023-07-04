using Shared.Models;
using Shared;

namespace DIVISOR_WASM.Server.Services.TeamService
{
    public interface ITeamService
    {
        Task<ServiceResponse<Team>> GetTeamAsync(int Id);
        Task<ServiceResponse<List<Team>>> GetAllTeamsAsync();
        Task<ServiceResponse<Team>> Save(Team Team);
        Task<ServiceResponse<Team>> Modify(Team Team);
        Task<ServiceResponse<Team>> Delete(int TeamId);
        Task<ServiceResponse<Boolean>> Shuffle();

    }
}
