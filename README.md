# school_manage_aspnetcore

 Author: [DUC CUONG BUI]
 Project: [school manage aspnetcore]
 GitHub Repository: [[Repository URL](https://github.com/bdcuongvn83/school_manage_aspnetcore.git)]
License: GPL License

# video Part1:
https://github.com/user-attachments/assets/46902163-2f01-4458-b94d-5b6df6d44fb5

# video Part2:

https://github.com/user-attachments/assets/967b685c-106e-4222-ac3d-1e2be7aa1371

The features of this software include:

## Basic Management Functions: Managing courses (table: Course).
## Student Management: Managing students (table: Student).
## Enrollment Management: Managing student enrollments (table: Enrollment).
## Instructor Management: Managing instructors (an instructor can teach multiple courses, tables: Instructor, CourseInstructor).
## Settings Management: Configuring page size and other settings (stored in session).

# Techniques used in this project:
## ASP.NET Core Razor Pages.
## Implementing Create, Read, Update, Delete (CRUD) operations using Entity Framework Core (EF Core).
## Form validation, property binding.
## Sorting headers, filtering/searching, and pagination (page size configuration).
## Database migrations, with the following commands executed via Package Manager Console (PMC):
Add-Migration InitialCreate
Drop-Database
Update-Database
## Updating related data (e.g., relationships between instructors and CourseInstructor detail tables).
