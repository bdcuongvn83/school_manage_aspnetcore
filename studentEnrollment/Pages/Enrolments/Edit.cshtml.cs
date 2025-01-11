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

namespace studentEnrollment.Pages.Enrolments
{
    public class EditModel : PageModel
    {
        private readonly studentEnrollment.Data.UniversityContext _context;

        public EditModel(studentEnrollment.Data.UniversityContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Enrolment Enrolment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrolment =  await _context.Enrolment.FirstOrDefaultAsync(m => m.ID == id);
            if (enrolment == null)
            {
                return NotFound();
            }
            Enrolment = enrolment;
           ViewData["CourseID"] = new SelectList(_context.Course.ToList(), "ID", "Title");
           ViewData["StudentID"] = new SelectList(_context.Student, "ID", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Enrolment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrolmentExists(Enrolment.ID))
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

        private bool EnrolmentExists(int id)
        {
            return _context.Enrolment.Any(e => e.ID == id);
        }
    }
}
