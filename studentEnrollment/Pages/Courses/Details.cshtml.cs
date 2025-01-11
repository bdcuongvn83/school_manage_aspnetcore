using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using studentEnrollment.Data;
using studentEnrollment.Pages.Models;

namespace studentEnrollment.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly studentEnrollment.Data.UniversityContext _context;

        public DetailsModel(studentEnrollment.Data.UniversityContext context)
        {
            _context = context;
        }

        public Course Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FirstOrDefaultAsync(m => m.ID == id);

            if (course is not null)
            {
                Course = course;

                return Page();
            }

            return NotFound();
        }
    }
}
