using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace studentEnrollment.Pages.Models
{
    [Table("CourseInstructor")]
    public class CourseInstructor
    {
        //[Key]  // Đánh dấu khóa chính
        [Column(name: "InstructorsID")]
        public int InstructorsID { get; set; }

        //[Key]  // Đánh dấu khóa chính
        [Column(name: "CoursesID")]
        public int CoursesID { get; set; }

        public Instructor Instructor { get; set; }
        public Course Course { get; set; }
    }
}
