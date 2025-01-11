using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using studentEnrollment.Data;
using studentEnrollment.Pages.Models;

namespace studentEnrollment.Pages.Instructors
{
    public class CreateModel : InstructorCoursesPageModel
    {
        private readonly studentEnrollment.Data.UniversityContext _context;
        private readonly ILogger<InstructorCoursesPageModel> _logger;

        [BindProperty]
        public Instructor Instructor { get; set; } = default!;

        public CreateModel(studentEnrollment.Data.UniversityContext context, ILogger<InstructorCoursesPageModel> logger)
        {
            _context = context;
            Instructor = new Instructor();
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {

            PopulateAssignedCourseData(_context, Instructor);

            return Page();

        }


        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string[] selectedCourses)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            _context.Instructors.Add(Instructor);
            _context.SaveChanges();

            if (selectedCourses.Length>0)
            { 
                foreach (var itemId in selectedCourses)
                {
                    var foundCourse = await _context.Course.FindAsync(Convert.ToInt32(itemId));
                    if (foundCourse!=null)
                    {
                        CourseInstructor item = new CourseInstructor();
                        item.CoursesID = Convert.ToInt32(itemId);
                        item.InstructorsID = Instructor.ID;
                        _context.CourseInstructors.Add(item);

                    }
                    else
                    {
                        _logger.LogWarning("Course {course} not found", itemId);
                    }

                }
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
