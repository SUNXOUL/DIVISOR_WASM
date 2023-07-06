using Shared;
using Shared.Models;
namespace DIVISOR_WASM.Client.Services.StudentService
{
    public interface IStudentService
    {
        List<Student> StudentList { get; set; }
        Task<Student> GetStudent(int Id);
        Task<ServiceResponse<Student>> Save(Student Student);
        Task GetList();
        Task<ServiceResponse<string>> Delete(int Id);
    }
}
