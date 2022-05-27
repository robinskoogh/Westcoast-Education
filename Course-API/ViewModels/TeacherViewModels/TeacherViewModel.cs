using Course_API.Models;
using Course_API.ViewModels.AuthViewModels;

namespace Course_API.ViewModels.TeacherViewModels
{
    public class TeacherViewModel : UserViewModel
    {
        public List<string>? AreasOfExpertise { get; set; }
    }
}