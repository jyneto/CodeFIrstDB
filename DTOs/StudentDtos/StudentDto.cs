using System.ComponentModel.DataAnnotations;

namespace CodeFIrstDB.DTOs.StudentDtos
{
    public class StudentDto
    {
        [MinLength(5)]
        [MaxLength(50)]
        public string StudentName { get; set; }
        public string StudentLastName { get; set; }

        [EmailAddress]
        public string StudentEmail { get; set; }

        //public int ClassId { get; set; }
    }

}
