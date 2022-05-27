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
        private readonly UserManager<AppUser> _userManager;
        public AuthController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserViewModel>> RegisterUser(RegisterUserViewModel user)
        {
            if (await _userManager.FindByEmailAsync(user.Email) is not null)
                return BadRequest("There is already a user with that email in the database");

            var newUser = new AppUser
            {
                UserName = user.Email!.ToLower(),
                Email = user.Email.ToLower(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                StreetAddress = user.StreetAddress,
                ZipCode = user.ZipCode,
                City = user.City,
                Courses = new List<Course>()
            };

            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (result.Succeeded)
            {
                var userViewModel = new UserViewModel
                {
                    Username = newUser.UserName,
                    Name = string.Concat(newUser.FirstName, " ", newUser.LastName),
                    Address = string.Concat(newUser.StreetAddress, ", ", newUser.ZipCode, ", ", newUser.City),
                    PhoneNumber = newUser.PhoneNumber
                };

                return StatusCode(201, userViewModel);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("User registration", error.Description);
                }

                return StatusCode(500, ModelState);
            }
        }
    }
}