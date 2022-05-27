using Course_API.Models;
using Course_API.ViewModels.AuthViewModels;

namespace Course_API.ViewModels.TeacherViewModels
{
    public class PostTeacherViewModel : RegisterUserViewModel
    {
        public List<string> AreasOfExpertise { get; set; } = new();
    }
}