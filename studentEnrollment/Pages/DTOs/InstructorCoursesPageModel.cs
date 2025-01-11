using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using studentEnrollment.Data;

namespace studentEnrollment.Pages.Models
{
    public class InstructorCoursesPageModel : PageModel

    {
       
        public List<AssignedCourseData> AssignedCourseDataList;

        public void PopulateAssignedCourseData(UniversityContext context,
                                               Instructor instructor)
        {
            var allCourses = context.Course;
            var instructorCourses = new HashSet<int>();
            if (instructor.CoursesInstructors != null && instructor.CoursesInstructors.Count > 0)
            {
                instructorCourses = new HashSet<int>(instructor.CoursesInstructors.Select(c => c.CoursesID));
            }

            AssignedCourseDataList = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                AssignedCourseDataList.Add(new AssignedCourseData
                {
                    CourseID = course.ID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.ID)
                });
            }
        }
    }
}
