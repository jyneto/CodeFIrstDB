using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFIrstDB.DTOs.StudentDtos
{
    public class StudentCreateDto
    {
        [MinLength(5)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string EmailAdress { get; set; }

        public int ClassId { get; set; }
    }
}
