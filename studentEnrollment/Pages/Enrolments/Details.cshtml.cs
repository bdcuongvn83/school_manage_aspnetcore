using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using studentEnrollment.Data;
using studentEnrollment.Pages.Models;

namespace studentEnrollment.Pages.Enrolments
{
    public class DetailsModel : PageModel
    {
        private readonly studentEnrollment.Data.UniversityContext _context;

        public DetailsModel(studentEnrollment.Data.UniversityContext context)
        {
            _context = context;
        }

        public Enrolment Enrolment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrolment = await _context.Enrolment.Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (enrolment is not null)
            {
                Enrolment = enrolment;
              
                return Page();
            }

            return NotFound();
        }
    }
}
