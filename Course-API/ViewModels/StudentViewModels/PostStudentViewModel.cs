using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course_API.Models;
using Course_API.ViewModels.AuthViewModels;

namespace Course_API.ViewModels.StudentViewModels
{
    public class PostStudentViewModel : RegisterUserViewModel
    {
        public List<Course> Courses { get; set; } = new();
    }
}