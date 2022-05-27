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
        public Task<TeacherViewModel?> GetTeacherByIdAsync(string id);
        public Task<TeacherViewModel?> GetTeacherByEmailAsync(string email);
        public Task AddTeacherAsync(PostTeacherViewModel teacher);
        public Task UpdateTeacherAsync(string id, PostTeacherViewModel teacher);
        public Task DeleteTeacherByIdAsync(string id);
        public Task DeleteTeacherByEmailAsync(string email);
    }
}