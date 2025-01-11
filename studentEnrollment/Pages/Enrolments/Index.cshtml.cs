using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using studentEnrollment.Data;
using studentEnrollment.Pages.Common;
using studentEnrollment.Pages.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace studentEnrollment.Pages.Enrolments
{
    public class IndexModel : PageModel
    {
        private readonly studentEnrollment.Data.UniversityContext _context;
        private readonly IConfiguration _configuration;
        private List<Enrolment> items;

        [BindProperty(SupportsGet  = true)]
        public string StudentName { get; set; } = "";

        //----------sort-----------------------------------

        public string StudentNameSort { get; set; }
        public string TitleSort { get; set; }
        public string GradeSort { get; set; }

        //public PaginatedList<EnrolmentLst> EnrolmentLst { get; set; }

        
        public int CurrentPage { get; set; }
        public string CurrentFilter { get; set; }//studentName
        public string CurrentSort { get; set; }//sortOrder
        public bool hasPreviousPage { get; set; }
        public bool hasNextPage { get; set; }
        public int PageSize
        {
            get
            {
                string PageSizeVal = HttpContext.Session.GetString("PageSize") ?? "3";

                return Convert.ToInt16(PageSizeVal);
            }
            
        }



        public IndexModel(studentEnrollment.Data.UniversityContext context, IConfiguration config)
        {
            _context = context;
            _configuration = config;

            // using System;
            StudentNameSort = string.IsNullOrEmpty(StudentNameSort) ? "name_desc" : "";
            TitleSort = string.IsNullOrEmpty(TitleSort) ? "title_desc" : "";
            GradeSort = string.IsNullOrEmpty(GradeSort) ? "grade_desc" : "";
            //PageSize = Convert.ToInt16(_configuration.GetConnectionString("PageSize"));
            //default if PageSize not found, set 3. PageSize can be setted in Common/SettingScreen
            
        }

        //public IList<EnrolmentLst> EnrolmentLst { get;set; } = default!;
        public PaginatedList<Enrolment> EnrolmentLst { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(string sortOrder, string currentFilter)

        {
            //case click header link/ search
            

            Console.WriteLine("sortOrder", sortOrder);
            CurrentPage = 1;
            if (!nullOrBlank(sortOrder))
            {
                CurrentSort = sortOrder;
            }

            if (nullOrBlank(CurrentSort)) {
                //set default
                CurrentSort = "name_desc";
            }

            await search(CurrentSort, currentFilter, CurrentPage);
            return Page(); // Không chuyển hướng

        }

        public async Task<IActionResult> OnGetSearchAsync(string sortOrder, string currentFilter)

        {
            //case click header link/ search

            Console.WriteLine("sortOrder", sortOrder);

            //using condition search on screen.
            if (!nullOrBlank(StudentName))
            {
                CurrentFilter = StudentName;
            }

            //set default sort order
            if (!nullOrBlank(sortOrder))
            {
                CurrentSort = sortOrder;
            }

            if (nullOrBlank(CurrentSort))
            {
                //set default
                CurrentSort = "name_desc";
            }

            int pageIndex = 1;//reset
            await search(CurrentSort, CurrentFilter, pageIndex);

            CurrentPage = pageIndex;
            
            return Page(); // Không chuyển hướng

        }

        public async Task<IActionResult> OnPostNextPageAsync(int currentPage, string sortOrder, string currentFilter)
        {
            Console.WriteLine("sortOrder", sortOrder);
            int pageIndex = currentPage + 1;
            await search(sortOrder, currentFilter, pageIndex);
            CurrentPage = pageIndex;
            CurrentSort = sortOrder;
            CurrentFilter = currentFilter;

            return Page(); // Không chuyển hướng

        }
        public async Task<IActionResult> OnPostPrevPageAsync(int currentPage, string sortOrder, string currentFilter)
        {
            Console.WriteLine("sortOrder", sortOrder);
            int pageIndex = currentPage - 1;
            if (pageIndex <= 0)
            {
                pageIndex = 1;

            }
            await search(sortOrder, currentFilter, pageIndex);
            CurrentPage = pageIndex;
            CurrentSort = sortOrder;
            CurrentFilter = currentFilter;
            return Page(); // Không chuyển hướng
        }

       
        private async Task search(string sortOrder, string StudentName, int pageIndex)
        {
            var query = _context.Enrolment.AsQueryable();

            query = query.Include(e => e.Course)
                .Include(e => e.Student);
            if (!nullOrBlank(StudentName))
            {
                query = query.Where(e => e.Student.FirstMidName.Contains(StudentName)
                || e.Student.LastName.Contains(StudentName));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(s => s.Student.FirstMidName);
                    break;
                case "title_desc":
                    query = query.OrderBy(s => s.Course.Title);
                    break;
                case "grade_desc":
                    query = query.OrderByDescending(s => s.grade);
                    break;
                default:
                    query = query.OrderByDescending(s => s.Student.FirstMidName);
                    break;
            }
            //EnrolmentLst = await query.ToListAsync();
            // int PageIndex = PageIndex != 0 ? PageIndex : 1;

            EnrolmentLst = await PaginatedList<Enrolment>.CreateAsync(
                query.AsNoTracking(), pageIndex, PageSize);

            
            Console.WriteLine("abc"); 
        }

        private bool nullOrBlank(string val)
        {
            return val == null || val.Length == 0;
        }
    }
}
