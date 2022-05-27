using Microsoft.AspNetCore.Identity;

namespace Course_API.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? StreetAddress { get; set; }
        public int ZipCode { get; set; }
        public string? City { get; set; }
        public ICollection<Course>? Courses { get; set; }
        public ICollection<Category>? AreasOfExpertise { get; set; }
    }
}