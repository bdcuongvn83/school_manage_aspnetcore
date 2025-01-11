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

# ERD structure:


# Detail features:

## 1. Basic Management Functions: Managing courses (table: Course).
![image](https://github.com/user-attachments/assets/07d7fe5a-1761-43c3-9ddb-177dd2891ee7)

Create new Course:
![image](https://github.com/user-attachments/assets/b58b88b9-a280-4b2a-8220-4d341cf060f8)

Result after register Course:
![image](https://github.com/user-attachments/assets/3d74d5f0-b72a-4bc0-9e8a-c65993912033)
Edit/delete Course
![image](https://github.com/user-attachments/assets/494a313e-92ac-450c-9d7d-91f56e391c9f)

Delete Course:
![image](https://github.com/user-attachments/assets/d6987a63-49fd-439f-954c-fc496fff7cf9)

## 2. Student Management: Managing students (table: Student).(similar Course Management)

![image](https://github.com/user-attachments/assets/123b69bb-37ff-4e30-89cb-c0b5a2ba7f93)
### Validate form if error:

![image](https://github.com/user-attachments/assets/b31fd217-ec9c-47d2-a2d5-119d49195d91)

## 3. Enrollment Management: Managing student enrollments (table: Enrollment).
This page support search filter by basic condition search (by student name), and support header sort, and paging. 

![image](https://github.com/user-attachments/assets/159bf924-37bc-46ce-872d-880d6db6bf62)
Search by name:
![image](https://github.com/user-attachments/assets/32accf0b-167e-4734-bf73-b950ec6fcefa)

Sort by header:
![image](https://github.com/user-attachments/assets/af5e815a-0b28-43ad-8313-a33dbea8d4fb)

Pageing: adjust setting pagesize, and paging.
![image](https://github.com/user-attachments/assets/11a4ddc2-2e33-415d-a973-591de64c2a13)

![image](https://github.com/user-attachments/assets/84dd1d19-8f1a-4969-87d5-3cfa0f76b72f)

Click next page
![image](https://github.com/user-attachments/assets/14e68cc2-5ee9-4f16-a4fa-43c5ee6cf25f)

## 4. Instructor Management
![image](https://github.com/user-attachments/assets/dcd628a0-b341-4395-a4e1-b38b8458f889)


-Create/update/delete record relating 2 tables Parent table, and detail Table.
![image](https://github.com/user-attachments/assets/e2156564-c72d-4aa8-bb11-209c383eb500)

![image](https://github.com/user-attachments/assets/1fc3225f-7399-4ce2-85f8-4c7e6beb0946)




