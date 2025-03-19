using System.ComponentModel.DataAnnotations;

namespace CodeFIrstDB.Properties.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }
        public string Name { get; set; }
        public virtual List<ClassCourse> ClassCourses{get; set;}

    }
}
