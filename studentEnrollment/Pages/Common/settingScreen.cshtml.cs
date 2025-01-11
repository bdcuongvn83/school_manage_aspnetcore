using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace studentEnrollment.Pages.Common
{
    public class SettingScreen : PageModel
    {
        [BindProperty]
        public string PageSize { get; set; } = "3";


        public List<SelectListItem> Options { get; set; } = new List<SelectListItem>();


        public IActionResult OnGet(string message)
        {
            // PageSize = TempData["PageSize"]!=null ? TempData["PageSize"].ToString(): "3";
            PageSize = HttpContext.Session.GetString("PageSize") ?? "3";
            ViewData["Message"] = message;//get messsageVal from redirectPage

            Options = new List<SelectListItem>
                {
                    new SelectListItem { Text = "3", Value = "3"},
                    new SelectListItem { Text = "5", Value = "5" },
                    new SelectListItem { Text = "10", Value = "10" }
                };

            return Page();

        }
        public IActionResult OnPost()
        {

            //TempData["PageSize"] = PageSize;
            HttpContext.Session.SetString("PageSize", PageSize) ;
            string messsageVal = "Setup already success";
            ViewData["Message"] = messsageVal;

            return RedirectToPage(new { message = messsageVal });

        }
    }
}
