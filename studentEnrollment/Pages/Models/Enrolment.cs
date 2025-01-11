using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace studentEnrollment.Pages.Models
{
    [Table("Enrolment")]
    public class Enrolment
    {
        [Key]
        public int ID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }

        public double grade { get; set; }

        [ForeignKey("CourseID")]
        public Course? Course { get; set; }

        [ForeignKey("StudentID")]
        public Student? Student { get; set; }
    }
}
