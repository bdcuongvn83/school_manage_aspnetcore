using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using studentEnrollment.Pages.Models;

namespace studentEnrollment.Data
{
    public class UniversityContext : DbContext
    {
        public UniversityContext (DbContextOptions<UniversityContext> options)
            : base(options)
        {
        }

        public DbSet<studentEnrollment.Pages.Models.Student> Student { get; set; } = default!;
        public DbSet<studentEnrollment.Pages.Models.Course> Course { get; set; } = default!;
        public DbSet<studentEnrollment.Pages.Models.Enrolment> Enrolment { get; set; } = default!;
        public DbSet<Instructor> Instructors { get; set; } = default!;

        public DbSet<CourseInstructor> CourseInstructors { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instructor>().ToTable(nameof(Instructor))
                .HasMany(c => c.CoursesInstructors);
              

            modelBuilder.Entity<Student>().ToTable(nameof(Student));
            modelBuilder.Entity<Instructor>().ToTable(nameof(Instructor));

            modelBuilder.Entity<CourseInstructor>()
            .HasKey(ci => new { ci.InstructorsID, ci.CoursesID }); // Khóa chính composite với 2 trường

            modelBuilder.Entity<CourseInstructor>()
        .HasOne(ci => ci.Course)
        .WithMany(c => c.CoursesInstructors) // Sửa lại tên thuộc tính ở đây
        .HasForeignKey(ci => ci.CoursesID);

            modelBuilder.Entity<CourseInstructor>()
                .HasOne(ci => ci.Instructor)
                .WithMany(i => i.CoursesInstructors)
                .HasForeignKey(ci => ci.InstructorsID);

        }
    }
}
