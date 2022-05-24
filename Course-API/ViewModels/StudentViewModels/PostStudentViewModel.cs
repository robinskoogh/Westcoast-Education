using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course_API.ViewModels.StudentViewModels
{
    public class PostStudentViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StreetAddress { get; set; }
        public int ZipCode { get; set; }
        public string? City { get; set; }
    }
}