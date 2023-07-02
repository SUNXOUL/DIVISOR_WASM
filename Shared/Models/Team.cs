using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [Required(ErrorMessage = "The name of the team is obligatory")]
        public string? Name { get; set; }

        [ForeignKey("StudentId")]
        public List<Student> StudentList { get; set; } 
    } 
}