using Shared.Models;
using Shared;

namespace DIVISOR_WASM.Server.Services.StudentService
{
    public interface IStudentService
    {
        Task<ServiceResponse<Student>> GetStudentAsync(int Id);
        Task<ServiceResponse<List<Student>>> GetAllStudentsAsync();
        Task<ServiceResponse<Student>> Save(Student Student);
        Task<ServiceResponse<Student>> Modify(Student Student);
        Task<ServiceResponse<Student>> Delete(int StudentId);

    }
}

