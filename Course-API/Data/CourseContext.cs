using Course_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Course_API.Data
{
    public class CourseContext : IdentityDbContext<AppUser>
    {
        public DbSet<Course> Courses => Set<Course>();
        // public DbSet<Student> Students => Set<Student>();
        // public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Category> Categories => Set<Category>();

        public CourseContext(DbContextOptions options) : base(options)
        {
        }
    }
}