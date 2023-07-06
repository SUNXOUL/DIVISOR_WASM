using Microsoft.EntityFrameworkCore;
using DIVISOR_WASM.Server.DAL;
using Shared;
using Shared.Models;

namespace DIVISOR_WASM.Server.Services.TeamService
{
    public class TeamService : ITeamService
    {
        private Contexto _contexto { get; set; }

        public TeamService(Contexto contexto)
        {
            this._contexto = contexto;
        }

        public async Task<ServiceResponse<Team>> GetTeamAsync(int Id)
        {
            var response = new ServiceResponse<Team>();
            var Team = await _contexto.Teams.Include(o => o.StudentList).AsNoTracking().SingleOrDefaultAsync(o => o.TeamId == Id);
            if (Team == null)
            {
                response.Success = false;
                response.Message = "This Team don't exist";
            }
            else
            {
                response.Data = Team;
            }

            return response;
        }

        public async Task<ServiceResponse<List<Team>>> GetAllTeamsAsync()
        {
            var response = new ServiceResponse<List<Team>>();
            response.Data = await _contexto.Teams.Include(o => o.StudentList).ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<Team>> Save(Team Team)
        {
            if (await Exist(Team.TeamId))
                return await Modify(Team);
            else
                return await Insert(Team);
        }

        public Task<bool> Exist(int TeamId)
        {
            return _contexto.Teams.AnyAsync(o => o.TeamId == TeamId);
        }

        private async Task<ServiceResponse<Team>> Insert(Team Team)
        {
            var response = new ServiceResponse<Team>();
            try
            {
                if (Team != null)
                {
                    _contexto.Teams.Add(Team);
                    bool guardado = await _contexto.SaveChangesAsync() > 0;
                    _contexto.Entry(Team).State = EntityState.Detached;
                    response.Data = Team;
                    response.Success = guardado;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Team not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Data = Team;
            }
            return response;
        }

        public async Task<ServiceResponse<Team>> Modify(Team Team)
        {
            var response = new ServiceResponse<Team>();
            try
            {
                if (Team != null)
                {
                    _contexto.Update(Team);
                    var guardo = await _contexto.SaveChangesAsync() > 0;
                    _contexto.Entry(Team).State = EntityState.Detached;
                    response.Data = Team;
                    response.Success = guardo;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Team not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Data = Team;
            }
            return response;
        }
        public async Task<ServiceResponse<Team>> Delete(int TeamId)
        {
            var response = new ServiceResponse<Team>();
            var Team = await _contexto.Teams.FindAsync(TeamId);
            try
            {
                if (Team != null)
                {
                    _contexto.Remove(Team);
                    bool guardado = await _contexto.SaveChangesAsync() > 0;
                    response.Data = Team;
                    response.Success = guardado;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Team not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Data = Team;
            }
            return response;
        }

        public async Task<ServiceResponse<Boolean>> Shuffle()
        {
            IEnumerable<Team> Teams = await _contexto.Teams.ToListAsync();
            List<Student> Students = await _contexto.Students.ToListAsync();
            Random rand = new Random();
            Student student = new Student();
            int index = 0;
            var response = new ServiceResponse<Boolean>();
            try
            {
                foreach (var team in Teams)
                {
                    team.StudentList = new List<Student>();
                }

                while (Students.Count() > 0)
                {
                    foreach (var team in Teams)
                    {
                        if (Students.Count() > 0)
                        {
                            index = rand.Next(Students.Count());
                            student = Students[index];
                            team.StudentList.Add(student);
                            Students.Remove(student);
                            _contexto.Teams.Update(team);
                        }
                    }
                }
                var guardo = await _contexto.SaveChangesAsync() > 0;
                response.Data = guardo;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Data = false;
            }
            return response;
        }
    }
}