using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Course_API.Data;
using Course_API.Interfaces;
using Course_API.Models;
using Course_API.ViewModels.TeacherViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Course_API.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly CourseContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public TeacherRepository(CourseContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<TeacherViewModel>> ListTeachersAsync()
        {
            var users = await _context.Users.Include(t => t.AreasOfExpertise).ToListAsync();

            var teacherClaims = await _userManager.GetUsersForClaimAsync(new Claim("Teacher", "true"));

            List<AppUser> teachers = new();

            foreach (var teacher in teacherClaims)
            {
                var teacherWithClaim = users.Where(t => t.Id == teacher.Id).SingleOrDefault();

                teachers.Add(teacherWithClaim!);
            }

            List<TeacherViewModel> teacherViewModels = new();

            foreach (var teacher in teachers)
            {
                List<string> areasOfExpertise = new();

                foreach (var aoe in teacher.AreasOfExpertise!)
                {
                    areasOfExpertise.Add(aoe.Name!);
                }

                var teacherViewModel = _mapper.Map<TeacherViewModel>(teacher);

                teacherViewModel.AreasOfExpertise = areasOfExpertise;

                teacherViewModels.Add(teacherViewModel);
            }

            return teacherViewModels;
        }

        public async Task<TeacherViewModel?> GetTeacherByIdAsync(string id)
        {
            var teacher = await _context.Users.Include(t => t.AreasOfExpertise).SingleOrDefaultAsync(t => t.Id == id);

            if (teacher is null) return null;

            List<string> areasOfExpertise = new();

            foreach (var aoe in teacher.AreasOfExpertise!)
            {
                areasOfExpertise.Add(aoe.Name!);
            }

            var teacherViewModel = _mapper.Map<TeacherViewModel>(teacher);

            teacherViewModel.AreasOfExpertise = areasOfExpertise;

            return teacherViewModel;
        }

        public async Task<TeacherViewModel?> GetTeacherByEmailAsync(string email)
        {
            var teacher = await _context.Users.Include(t => t.AreasOfExpertise).SingleOrDefaultAsync(t => t.Email == email);

            if (teacher is null) return null;

            List<string> areasOfExpertise = new();

            foreach (var aoe in teacher.AreasOfExpertise!)
            {
                areasOfExpertise.Add(aoe.Name!);
            }

            var teacherViewModel = _mapper.Map<TeacherViewModel>(teacher);

            teacherViewModel.AreasOfExpertise = areasOfExpertise;

            return teacherViewModel;
        }

        public async Task AddTeacherAsync(PostTeacherViewModel teacher)
        {
            var teacherToAdd = _mapper.Map<AppUser>(teacher);

            List<Category> areasOfExpertise = new();

            foreach (var aoe in teacher.AreasOfExpertise)
            {
                var areaOfExpertise = await _context.Categories.SingleOrDefaultAsync(c => c.Name == aoe);

                if (areaOfExpertise is null)
                {
                    var newCategory = new Category
                    {
                        Name = aoe,
                    };

                    _context.Add(newCategory);

                    areaOfExpertise = newCategory;
                }

                areasOfExpertise.Add(areaOfExpertise);
            }

            teacherToAdd.AreasOfExpertise = areasOfExpertise;

            var result = await _userManager.CreateAsync(teacherToAdd, teacher.Password);

            if (!result.Succeeded)
                throw new Exception("An error occurred while attempting to create the teacher");

            await _userManager.AddClaimAsync(teacherToAdd, new Claim("Teacher", "true"));
        }

        public async Task UpdateTeacherAsync(string id, PostTeacherViewModel teacher)
        {
            var teacherToUpdate = await _context.Users.Include(t => t.AreasOfExpertise).Where(t => t.Id == id).SingleOrDefaultAsync();

            if (teacherToUpdate is null)
                throw new Exception($"There is no teacher with Id: {id} in the database");

            if (teacherToUpdate.Email != teacher.Email && await GetTeacherByEmailAsync(teacher.Email!) is not null)
                throw new Exception($"The email {teacher.Email} is already in use by another teacher");

            List<Category> areasOfExpertise = new();

            foreach (var aoe in teacher.AreasOfExpertise)
            {
                var areaOfExpertise = await _context.Categories.SingleOrDefaultAsync(c => c.Name == aoe);

                if (areaOfExpertise is null)
                {
                    var newCategory = new Category
                    {
                        Name = aoe,
                    };

                    _context.Add(newCategory);

                    areaOfExpertise = newCategory;
                }

                areasOfExpertise.Add(areaOfExpertise);
            }

            _mapper.Map(teacher, teacherToUpdate);

            teacherToUpdate.AreasOfExpertise = areasOfExpertise;

            var result = await _userManager.UpdateAsync(teacherToUpdate);

            if (!result.Succeeded)
                throw new Exception("An error occurred while attempting to update the teacher");
        }

        public async Task DeleteTeacherByIdAsync(string id)
        {
            var teacherToDelete = await _context.Users.FindAsync(id);

            if (teacherToDelete is null)
                throw new Exception($"There is no teacher with Id: {id} in the database");

            var result = await _userManager.DeleteAsync(teacherToDelete);

            if (!result.Succeeded)
                throw new Exception("An error occurred while attempting to delete the teacher");
        }

        public async Task DeleteTeacherByEmailAsync(string email)
        {
            var teacherToDelete = await _context.Users.Where(t => t.Email == email).SingleOrDefaultAsync();

            if (teacherToDelete is null)
                throw new Exception($"There is no teacher with Email: {email} in the database");

            var result = await _userManager.DeleteAsync(teacherToDelete);

            if (!result.Succeeded)
                throw new Exception("An error occurred while attempting to delete the teacher");
        }
    }
}