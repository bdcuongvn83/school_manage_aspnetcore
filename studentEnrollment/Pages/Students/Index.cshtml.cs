using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using studentEnrollment.Data;
using studentEnrollment.Pages.Models;

namespace studentEnrollment.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly studentEnrollment.Data.UniversityContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

     

        public IndexModel(studentEnrollment.Data.UniversityContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Student = await _context.Student.ToListAsync();
        }

        public IActionResult OnPostExportPdf()
        {
            Console.WriteLine("ExportPdf");
            // 1️⃣ Định vị file báo cáo RDLC
            string reportPath = Path.Combine(_webHostEnvironment.WebRootPath, "Reports", "Report.rdlc");

            // 2️⃣ Lấy dữ liệu từ database
            var students = _context.Student.ToList();  // Giả sử có danh sách sinh viên

            // 3️⃣ Chuyển đổi List<Student> thành DataTable
            DataTable studentTable = ConvertToDataTable(students);

            // 4️⃣ Khởi tạo báo cáo RDLC
            LocalReport report = new LocalReport(reportPath);

            // 5️⃣ Thêm nguồn dữ liệu vào báo cáo
            report.AddDataSource("MyDataSet", studentTable);

            // 6️⃣ Xuất báo cáo dưới dạng PDF
            string mimeType = "";
            int extension = 1;
            var result = report.Execute(RenderType.Pdf, extension, null, mimeType);

            byte[] pdfBytes = result.MainStream; // ✅ Lấy dữ liệu báo cáo

            // 7️⃣ Trả về file PDF
            return File(pdfBytes, "application/pdf", "StudentReport.pdf");

            //return Page(); // Không chuyển hướng

        }
        // 📌 Hàm chuyển đổi List<T> thành DataTable
        private DataTable ConvertToDataTable<T>(List<T> data)
        {
            DataTable table = new DataTable();
            var props = typeof(T).GetProperties();

            foreach (var prop in props)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (var item in data)
            {
                var values = props.Select(p => p.GetValue(item, null)).ToArray();
                table.Rows.Add(values);
            }

            return table;
        }
       
    }
}
