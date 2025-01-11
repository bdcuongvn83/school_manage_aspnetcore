using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using studentEnrollment.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<UniversityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UniversityContext") ?? throw new InvalidOperationException("Connection string 'UniversityContext' not found.")));

//TODO them service cho TempData
builder.Services.AddDistributedMemoryCache();  // Nếu bạn muốn dùng MemoryCache cho Session
builder.Services.AddSession();  // Thêm dịch vụ Session
builder.Services.AddRazorPages()
        .AddSessionStateTempDataProvider();  // Cấu hình TempData sử dụng Session

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Cấu hình middleware
app.UseSession();  // Bắt buộc phải thêm middleware này

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
