using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Course_API.Data;
using Course_API.Interfaces;
using Course_API.Models;
using Course_API.ViewModels.StudentViewModels;
using Microsoft.EntityFrameworkCore;

namespace Course_API.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CourseContext _context;
        private readonly IMapper _mapper;
        public StudentRepository(CourseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<StudentViewModel>> ListStudentsAsync() =>
            await _context.Students.ProjectTo<StudentViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

        public async Task<StudentViewModel?> GetStudentByIdAsync(int id) =>
            await _context.Students.Include(s => s.Courses)
                .Where(s => s.Id == id).ProjectTo<StudentViewModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync();

        public async Task<StudentViewModel?> GetStudentByEmailAsync(string email) =>
            await _context.Students.Include(s => s.Courses)
                .Where(s => s.Email == email).ProjectTo<StudentViewModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync();

        public async Task AddStudentAsync(PostStudentViewModel student)
        {
            var studentToAdd = _mapper.Map<Student>(student);

            await _context.Students.AddAsync(studentToAdd);
        }

        public async Task UpdateStudentAsync(int id, PostStudentViewModel student)
        {
            var studentToUpdate = await _context.Students.FindAsync(id);

            if (studentToUpdate is null)
                throw new Exception($"There is no student with Id: {id} in the database");

            if (student.Email != studentToUpdate.Email && await GetStudentByEmailAsync(student.Email!) is not null)
                throw new Exception($"The email {student.Email} is already in use by another student");

            _mapper.Map(student, studentToUpdate);

            _context.Update(studentToUpdate);
        }

        public async Task DeleteStudentByIdAsync(int id)
        {
            var studentToDelete = await _context.Students.FindAsync(id);

            if (studentToDelete is null)
                throw new Exception($"There is no student with Id: {id} in the database");

            _context.Remove(studentToDelete);
        }

        public async Task DeleteStudentByEmailAsync(string email)
        {
            var studentToDelete = await _context.Students.Where(s => s.Email == email).SingleOrDefaultAsync();

            if (studentToDelete is null)
                throw new Exception($"There is no student with the email: {email} in the database");

            _context.Remove(studentToDelete);
        }

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
    }
}