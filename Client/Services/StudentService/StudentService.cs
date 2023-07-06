using Shared;
using Shared.Models;
using System.Net.Http.Json;

namespace DIVISOR_WASM.Client.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _http;

        public StudentService(HttpClient http)
        {
            _http = http;
        }

        public List<Student> StudentList { get; set; } = new List<Student>();


        public async Task<ServiceResponse<string>> Delete(int Id)
        {
            var response = await _http.DeleteAsync($"api/Student/{Id}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return new ServiceResponse<string> { Success = true, Data = result };
        }

        public async Task<Student> GetStudent(int Id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Student>>($"api/Student/{Id}");

            return result.Data;

        }

        public async Task GetList()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Student>>>($"api/Student");

            if (result != null && result.Data != null)
            {
                StudentList = result.Data;
            }

        }


        public async Task<ServiceResponse<Student>> Save(Student Student)
        {
            var post = await _http.PostAsJsonAsync("api/Student", Student);
            var result = await post.Content.ReadFromJsonAsync<Student>();
            var response = new ServiceResponse<Student>();
            response.Data = result;

            return response;
        }

    }
}
