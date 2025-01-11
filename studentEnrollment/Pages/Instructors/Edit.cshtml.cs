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
    public class EditModel : InstructorCoursesPageModel
    {
        private readonly studentEnrollment.Data.UniversityContext _context;
        private readonly ILogger<InstructorCoursesPageModel> _logger;

        public EditModel(studentEnrollment.Data.UniversityContext context, ILogger<InstructorCoursesPageModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Instructor Instructor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor =  await _context.Instructors.Include(ci => ci.CoursesInstructors)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instructor == null)
            {
                return NotFound();
            }
            Instructor = instructor;
            PopulateAssignedCourseData(_context, Instructor);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string[] selectedCourses)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Instructor).State = EntityState.Modified;



            try
            {
                //await _context.SaveChangesAsync();

                var updateInstructor = await _context.Instructors.Include(c => c.CoursesInstructors).FirstOrDefaultAsync(m => m.ID == Instructor.ID);
                //remove all details
                updateInstructor.FirstMidName = Instructor.FirstMidName;
                updateInstructor.LastName = Instructor.LastName;
                updateInstructor.HireDate = Instructor.HireDate;

                _context.CourseInstructors.RemoveRange(updateInstructor.CoursesInstructors);
                _context.Instructors.Update(Instructor);
                _context.SaveChanges();
    
                //insert new details
                if (selectedCourses.Length > 0)
                {
                    foreach (var itemId in selectedCourses)
                    {
                        var foundCourse = await _context.Course.FindAsync(Convert.ToInt32(itemId));
                        if (foundCourse != null)
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


            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstructorExists(Instructor.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.ID == id);
        }
    }
}
