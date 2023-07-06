using Shared;
using Shared.Models;
using System.Net.Http.Json;

namespace DIVISOR_WASM.Client.Services.TeamService
{
    public class TeamService : ITeamService
    {
        private readonly HttpClient _http;

        public TeamService(HttpClient http)
        {
            _http = http;
        }

        public List<Team> TeamList { get; set; } = new List<Team>();


        public async Task<ServiceResponse<string>> Delete(int Id)
        {
            var response = await _http.DeleteAsync($"api/Team/{Id}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return new ServiceResponse<string> { Success = true, Data = result };
        }

        public async Task<Team> GetTeam(int Id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Team>>($"api/Team/{Id}");
            return result.Data;
        }

        public async Task GetList()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Team>>>($"api/Team");
            if (result != null && result.Data != null)
            {
                TeamList = result.Data;
            }
        }


        public async Task<ServiceResponse<Team>> Save(Team Team)
        {
            var post = await _http.PostAsJsonAsync("api/Team", Team);
            var result = await post.Content.ReadFromJsonAsync<Team>();
            var response = new ServiceResponse<Team>();
            response.Data = result;
            return response;
        }
    }
}
