using AutoMapper;
using AutoMapper.QueryableExtensions;
using Course_API.Data;
using Course_API.Interfaces;
using Course_API.Models;
using Course_API.ViewModels.CategoryViewModels;
using Microsoft.EntityFrameworkCore;

namespace Course_API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CourseContext _context;
        private readonly IMapper _mapper;
        public CategoryRepository(CourseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }
        public async Task<List<CategoryViewModel>> ListCategoriesAsync() =>
            await _context.Categories
                .OrderBy(c => c.Name)
                .ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

        public async Task AddCategoryAsync(CategoryViewModel category)
        {
            if (await _context.Categories.Where(c => c.Name!.ToLower() == category.Name!.ToLower()).SingleOrDefaultAsync() is not null)
                throw new Exception($"The category \"{category.Name}\" already exists in the database");

            var categoryToAdd = new Category
            {
                Name = category.Name
            };

            _context.Categories.Add(categoryToAdd);
        }

        public async Task DeleteCategoryAsync(string name)
        {
            var category = await _context.Categories
                .Include(c => c.Courses)
                .Include(c => c.Teachers)
                    .Where(c => c.Name!.ToLower() == name.ToLower())
                        .SingleOrDefaultAsync();

            if (category is null)
                throw new Exception("There is no category named \"{name}\" in the database");

            if (category.Courses.Count > 0 || category.Teachers.Count > 0)
                throw new Exception("This category is in use and can not be deleted");

            _context.Categories.Remove(category);
        }

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
    }
}