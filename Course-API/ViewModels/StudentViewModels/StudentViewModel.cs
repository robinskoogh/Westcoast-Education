using Course_API.Models;
using Course_API.ViewModels.AuthViewModels;

namespace Course_API.ViewModels.StudentViewModels
{
    public class StudentViewModel : UserViewModel
    {
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}