using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course_API.Models;
using Course_API.ViewModels.AuthViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Course_API.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AuthController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        // public async Task<ActionResult<UserViewModel>> CreateUser(PostUserViewModel user)
        // {
        //     if (await _userManager.FindByEmailAsync(user.Email) is not null)
        //         return BadRequest("There is already a user with that email in the database");

        //     var newUser = new Student
        //     {
        //         UserName = user.Email!.ToLower(),
        //         Email = user.Email.ToLower(),
        //         FirstName = user.FirstName,
        //         LastName = user.LastName,
        //         PhoneNumber = user.PhoneNumber,
        //         StreetAddress = user.StreetAddress,
        //         ZipCode = user.ZipCode,
        //         City = user.City
        //     };

        //     var result = await _userManager.CreateAsync(newUser, user.Password);
        // }
    }
}