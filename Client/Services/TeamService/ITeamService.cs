using Shared;
using Shared.Models;
namespace DIVISOR_WASM.Client.Services.TeamService
{
    public interface ITeamService
    {
        List<Team> TeamList { get; set; }
        Task<Team> GetTeam(int Id);
        Task<ServiceResponse<Team>> Save(Team Team);
        Task GetList();
        Task<ServiceResponse<string>> Delete(int Id);
        Task<ServiceResponse<Boolean>> Shuffle();
    }
}
