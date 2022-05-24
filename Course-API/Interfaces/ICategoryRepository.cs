using Course_API.ViewModels.CategoryViewModels;

namespace Course_API.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<CategoryViewModel>> ListCategoriesAsync();
        public Task AddCategoryAsync(CategoryViewModel category);
        public Task DeleteCategoryAsync(string name);
        public Task<bool> SaveChangesAsync();
    }
}