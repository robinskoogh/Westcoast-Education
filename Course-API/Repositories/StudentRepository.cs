using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Course_API.Data;
using Course_API.Interfaces;
using Course_API.Models;
using Course_API.ViewModels.StudentViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Course_API.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CourseContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public StudentRepository(CourseContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<StudentViewModel>> ListStudentsAsync()
        {
            var users = await _context.Users.Include(s => s.Courses).ToListAsync();

            var studentClaims = await _userManager.GetUsersForClaimAsync(new Claim("Student", "true"));

            List<AppUser> students = new();

            foreach (var student in studentClaims)
            {
                var studentWithClaim = users.Where(t => t.Id == student.Id).SingleOrDefault();

                students.Add(studentWithClaim!);
            }

            return _mapper.Map<IList<AppUser>, List<StudentViewModel>>(students);
        }

        public async Task<StudentViewModel?> GetStudentByIdAsync(string id) =>
            await _context.Users.Include(s => s.Courses)
                .Where(s => s.Id == id).ProjectTo<StudentViewModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync();

        public async Task<StudentViewModel?> GetStudentByEmailAsync(string email) =>
            await _context.Users.Include(s => s.Courses)
                .Where(s => s.Email == email).ProjectTo<StudentViewModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync();

        public async Task AddStudentAsync(PostStudentViewModel student)
        {
            var studentToAdd = _mapper.Map<AppUser>(student);

            var result = await _userManager.CreateAsync(studentToAdd, student.Password);

            if (!result.Succeeded)
                throw new Exception("An error occurred while attempting to create the student");

            await _userManager.AddClaimAsync(studentToAdd, new Claim("Student", "true"));
        }

        public async Task UpdateStudentAsync(string id, PostStudentViewModel student)
        {
            var studentToUpdate = await _context.Users.FindAsync(id);

            if (studentToUpdate is null)
                throw new Exception($"There is no student with Id: {id} in the database");

            if (student.Email != studentToUpdate.Email && await GetStudentByEmailAsync(student.Email!) is not null)
                throw new Exception($"The email {student.Email} is already in use by another student");

            _mapper.Map(student, studentToUpdate);

            var result = await _userManager.UpdateAsync(studentToUpdate);

            if (!result.Succeeded)
                throw new Exception("An error occurred while attempting to update the user");
        }

        public async Task DeleteStudentByIdAsync(string id)
        {
            var studentToDelete = await _context.Users.FindAsync(id);

            if (studentToDelete is null)
                throw new Exception($"There is no student with Id: {id} in the database");

            var result = await _userManager.DeleteAsync(studentToDelete);

            if (!result.Succeeded)
                throw new Exception("An error occurred while attempting to delete the user");
        }

        public async Task DeleteStudentByEmailAsync(string email)
        {
            var studentToDelete = await _context.Users.Where(s => s.Email == email).SingleOrDefaultAsync();

            if (studentToDelete is null)
                throw new Exception($"There is no student with the email: {email} in the database");

            var result = await _userManager.DeleteAsync(studentToDelete);

            if (!result.Succeeded)
                throw new Exception("An error occurred while attempting to delete the user");
        }
    }
}