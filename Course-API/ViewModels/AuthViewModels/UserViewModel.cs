using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course_API.ViewModels.AuthViewModels
{
    public class UserViewModel
    {
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}