using System.Text.Json;
using Course_API.Models;
using Course_API.ViewModels.CourseViewModels;
using Course_API.ViewModels.TeacherViewModels;
using Microsoft.EntityFrameworkCore;

namespace Course_API.Data
{
    public class SeedDatabase
    {
        public static async Task SeedCourses(CourseContext context)
        {
            if (await context.Courses.AnyAsync())
                return;

            var courseData = await File.ReadAllTextAsync("Data/SeedData/courses.json");
            var courses = JsonSerializer.Deserialize<List<PostCourseViewModel>>(courseData);

            if (courses is null) return;

            foreach (var course in courses)
            {
                var category = await context.Categories.SingleOrDefaultAsync(c => c.Name.ToLower() == course.Category!.ToLower());
                if (category is not null)
                {
                    var newCourse = new Course()
                    {
                        CourseNo = course.CourseNo,
                        Name = course.Name,
                        Length = course.Length,
                        LengthUnit = course.LengthUnit,
                        Description = course.Description,
                        Details = course.Details,
                        Category = category
                    };

                    context.Courses.Add(newCourse);
                }
            }


            await context.SaveChangesAsync();
        }

        public static async Task SeedStudents(CourseContext context)
        {
            if (await context.Students.AnyAsync()) return;

            var studentData = await File.ReadAllTextAsync("Data/SeedData/students.json");
            var students = JsonSerializer.Deserialize<List<Student>>(studentData);

            await context.AddRangeAsync(students!);
            await context.SaveChangesAsync();
        }

        public static async Task SeedCategories(CourseContext context)
        {
            if (await context.Categories.AnyAsync()) return;

            var categoryData = await File.ReadAllTextAsync("Data/SeedData/categories.json");
            var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);

            await context.AddRangeAsync(categories!);
            await context.SaveChangesAsync();
        }

        public static async Task SeedTeachers(CourseContext context)
        {
            if (await context.Teachers.AnyAsync()) return;

            var teacherData = await File.ReadAllTextAsync("Data/SeedData/teachers.json");
            var teachers = JsonSerializer.Deserialize<List<PostTeacherViewModel>>(teacherData);

            if (teachers is null) return;

            foreach (var teacher in teachers)
            {
                List<Category> areasOfExpertise = new();

                foreach (var aoe in teacher.AreasOfExpertise)
                {
                    var areaOfExpertise = await context.Categories.SingleOrDefaultAsync(c => c.Name!.ToLower() == aoe.ToLower());

                    if (areaOfExpertise is not null)
                        areasOfExpertise.Add(areaOfExpertise);
                }

                var newTeacher = new Teacher()
                {
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Email = teacher.Email,
                    PhoneNumber = teacher.PhoneNumber,
                    StreetAddress = teacher.StreetAddress,
                    ZipCode = teacher.ZipCode,
                    City = teacher.City,
                    AreasOfExpertise = areasOfExpertise
                };

                context.Teachers.Add(newTeacher);
            }

            await context.SaveChangesAsync();
        }
    }
}