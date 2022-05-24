using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course_API.Models;
using Course_API.ViewModels.TeacherViewModels;

namespace Course_API.Interfaces
{
    public interface ITeacherRepository
    {
        public Task<List<TeacherViewModel>> ListTeachersAsync();
        public Task<TeacherViewModel?> GetTeacherByIdAsync(int id);
        public Task<TeacherViewModel?> GetTeacherByEmailAsync(string email);
        public Task AddTeacherAsync(PostTeacherViewModel teacher);
        public Task UpdateTeacherAsync(int id, PostTeacherViewModel teacher);
        public Task DeleteTeacherByIdAsync(int id);
        public Task DeleteTeacherByEmailAsync(string email);
        public Task<bool> SaveAllChangesAsync();
    }
}