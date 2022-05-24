using Course_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Course_API.Data
{
    public class CourseContext : IdentityDbContext
    {
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<StudentCourse> StudentCourses => Set<StudentCourse>();

        public CourseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StudentCourse>().HasKey(sc => new { sc.CourseId, sc.StudentId });
        }
    }
}