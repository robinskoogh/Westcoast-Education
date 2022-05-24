using Course_API.Models;

namespace Course_API.ViewModels.TeacherViewModels
{
    public class TeacherViewModel
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phonenumber { get; set; }
        public string? Address { get; set; }
        public List<string>? AreasOfExpertise { get; set; }
    }
}