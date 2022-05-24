using AutoMapper;
using AutoMapper.QueryableExtensions;
using Course_API.Data;
using Course_API.Interfaces;
using Course_API.Models;
using Course_API.ViewModels.TeacherViewModels;
using Microsoft.EntityFrameworkCore;

namespace Course_API.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly CourseContext _context;
        private readonly IMapper _mapper;
        public TeacherRepository(CourseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<TeacherViewModel>> ListTeachersAsync()
        {
            var teachers = await _context.Teachers.Include(t => t.AreasOfExpertise).ToListAsync();

            List<TeacherViewModel> teacherViewModels = new();

            foreach (var teacher in teachers)
            {
                List<string> areasOfExpertise = new();

                foreach (var aoe in teacher.AreasOfExpertise)
                {
                    areasOfExpertise.Add(aoe.Name!);
                }

                var teacherViewModel = _mapper.Map<TeacherViewModel>(teacher);

                teacherViewModel.AreasOfExpertise = areasOfExpertise;

                teacherViewModels.Add(teacherViewModel);
            }

            return teacherViewModels;
        }

        public async Task<TeacherViewModel?> GetTeacherByIdAsync(int id)
        {
            var teacher = await _context.Teachers.Include(t => t.AreasOfExpertise).SingleOrDefaultAsync(t => t.Id == id);

            if (teacher is null) return null;

            List<string> areasOfExpertise = new();

            foreach (var aoe in teacher.AreasOfExpertise)
            {
                areasOfExpertise.Add(aoe.Name!);
            }

            var teacherViewModel = _mapper.Map<TeacherViewModel>(teacher);

            teacherViewModel.AreasOfExpertise = areasOfExpertise;

            return teacherViewModel;

        }

        public async Task<TeacherViewModel?> GetTeacherByEmailAsync(string email)
        {
            var teacher = await _context.Teachers.Include(t => t.AreasOfExpertise).SingleOrDefaultAsync(t => t.Email == email);

            if (teacher is null) return null;

            List<string> areasOfExpertise = new();

            foreach (var aoe in teacher.AreasOfExpertise)
            {
                areasOfExpertise.Add(aoe.Name!);
            }

            var teacherViewModel = _mapper.Map<TeacherViewModel>(teacher);

            teacherViewModel.AreasOfExpertise = areasOfExpertise;

            return teacherViewModel;

        }

        public async Task AddTeacherAsync(PostTeacherViewModel teacher)
        {
            var teacherToAdd = _mapper.Map<Teacher>(teacher);

            if (await _context.Teachers.Where(t => t.Email == teacher.Email).SingleOrDefaultAsync() is not null)
                throw new Exception($"There is already a teacher using the Email: {teacher.Email} registered in the database");

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

            await _context.Teachers.AddAsync(teacherToAdd);
        }

        public async Task UpdateTeacherAsync(int id, PostTeacherViewModel teacher)
        {
            var teacherToUpdate = await _context.Teachers.Include(t => t.AreasOfExpertise).Where(t => t.Id == id).SingleOrDefaultAsync();

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

            _context.Update(teacherToUpdate);
        }

        public async Task DeleteTeacherByIdAsync(int id)
        {
            var teacherToDelete = await _context.Teachers.FindAsync(id);

            if (teacherToDelete is null)
                throw new Exception($"There is no teacher with Id: {id} in the database");

            _context.Remove(teacherToDelete);
        }

        public async Task DeleteTeacherByEmailAsync(string email)
        {
            var teacherToDelete = await _context.Teachers.Where(t => t.Email == email).SingleOrDefaultAsync();

            if (teacherToDelete is null)
                throw new Exception($"There is no teacher with Email: {email} in the database");

            _context.Remove(teacherToDelete);
        }

        public async Task<bool> SaveAllChangesAsync() => await _context.SaveChangesAsync() > 0;
    }
}