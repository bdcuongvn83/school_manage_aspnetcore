using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace studentEnrollment.Pages.Models
{

    [Table("Course")]
    public class Course
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        [ValidateNever]
        public ICollection<CourseInstructor> CoursesInstructors { get; set; } = new List<CourseInstructor>();
        
    }
}
