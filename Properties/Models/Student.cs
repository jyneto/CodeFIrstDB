using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFIrstDB.Properties.Models
{
    public class Student
    {
        [Key]
        public int ID  {get; set; }
       
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }
        public string Email { get; set; }

        [ForeignKey("Class")]
        public int? ClassID_FK { get; set; }

        public virtual Class Class { get; set; } // Virtual referens to ClassID_FK
    }
}
