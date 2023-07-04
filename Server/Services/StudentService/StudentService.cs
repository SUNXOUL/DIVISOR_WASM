using Microsoft.EntityFrameworkCore;
using DIVISOR_WASM.Server.DAL;
using Shared;
using Shared.Models;

namespace DIVISOR_WASM.Server.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private Contexto _contexto { get; set; }

        public StudentService(Contexto contexto)
        {
            this._contexto = contexto;
        }

        public async Task<ServiceResponse<Student>> GetStudentAsync(int Id)
        {
            var response = new ServiceResponse<Student>();
            var Student = await _contexto.Students.FindAsync(Id);
            if (Student == null)
            {
                response.Success = false;
                response.Message = "This Student don't exist";
            }
            else
            {
                response.Data = Student;
            }

            return response;
        }

        public async Task<ServiceResponse<List<Student>>> GetAllStudentsAsync()
        {
            var response = new ServiceResponse<List<Student>>();
            response.Data = await _contexto.Students.ToListAsync();
            return response;
        }


        public async Task<ServiceResponse<Student>> Save(Student student)
        {
            if (await Exist(student.StudentId))
                return await Modify(student);
            else
                return await Insert(student);
        }

        public Task<bool> Exist(int StudentId)
        {
            return _contexto.Students.AnyAsync(o => o.StudentId == StudentId);
        }

        private async Task<ServiceResponse<Student>> Insert(Student student)
        {
            var response = new ServiceResponse<Student>();
            try
            {
                if (student != null)
                {
                    _contexto.Students.Add(student);
                    bool guardado = await _contexto.SaveChangesAsync() > 0;
                    _contexto.Entry(student).State = EntityState.Detached;
                    response.Data = student;
                    response.Success = guardado;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Student not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Data = student;
            }
            return response;
        }

        public async Task<ServiceResponse<Student>> Modify(Student Student)
        {
            var response = new ServiceResponse<Student>();
            try
            {
                if (Student != null)
                {
                    _contexto.Update(Student);
                    var guardo = await _contexto.SaveChangesAsync() > 0;
                    _contexto.Entry(Student).State = EntityState.Detached;
                    response.Data = Student;
                    response.Success = guardo;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Student not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Data = Student;
            }
            return response;
        }
        public async Task<ServiceResponse<Student>> Delete(int StudentId)
        {
            var response = new ServiceResponse<Student>();
            var Student = await _contexto.Students.FindAsync(StudentId);
            try
            {
                if (Student != null)
                {
                    _contexto.Remove(Student);
                    bool guardado = await _contexto.SaveChangesAsync() > 0;
                    response.Data = Student;
                    response.Success = guardado;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Student not found";
                }
            }

            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Data = Student;
            }
            return response;

        }
    }
}