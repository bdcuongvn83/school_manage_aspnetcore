using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using studentEnrollment.Data;
using studentEnrollment.Pages.Models;

namespace studentEnrollment.Pages.Enrolments
{
    public class CreateModel : PageModel
    {
        private readonly studentEnrollment.Data.UniversityContext _context;

        public CreateModel(studentEnrollment.Data.UniversityContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CourseID"] = new SelectList(_context.Course, "ID", "Title");
            ViewData["StudentID"] = new SelectList(_context.Student, "ID", "FullName");
            return Page();
        }
    
        [BindProperty]
        public Enrolment Enrolment { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Enrolment.Add(Enrolment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
