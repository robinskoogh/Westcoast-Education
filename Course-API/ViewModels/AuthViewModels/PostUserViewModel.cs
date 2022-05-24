using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Course_API.ViewModels.AuthViewModels
{
    public class PostUserViewModel
    {
        [Required]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StreetAddress { get; set; }
        public int ZipCode { get; set; }
        public string? City { get; set; }
    }
}