using System.ComponentModel.DataAnnotations;


namespace Shared.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "The name of the student is obligatory")]
        public string? Name { get; set; }

    }
}