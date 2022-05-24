using Course_API.Models;

namespace Course_API.ViewModels.TeacherViewModels
{
    public class PostTeacherViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StreetAddress { get; set; }
        public int ZipCode { get; set; }
        public string? City { get; set; }
        public List<string> AreasOfExpertise { get; set; } = new();
    }
}