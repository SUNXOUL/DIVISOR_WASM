
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DIVISOR_WASM.Server.Services.StudentService;
using Shared.Models;
using Shared;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService StudentServices;
        public StudentController(IStudentService StudentService)
        {
            this.StudentServices = StudentService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Student>>>> GetAllStudentes()
        {
            var productos = await StudentServices.GetAllStudentsAsync();
            return Ok(productos);
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<ServiceResponse<Student>>> GetStudent(int ID)
        {
            var result = await StudentServices.GetStudentAsync(ID);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Student>>> Save(Student Student)
        {
            var result = await StudentServices.Save(Student);
            return Ok(result);
        }

        [HttpDelete("{StudentId}")]
        public async Task<ActionResult<ServiceResponse<Student>>> Delete(int StudentId)
        {
            var result = await StudentServices.Delete(StudentId);
            return Ok(result);
        }
    }
}
