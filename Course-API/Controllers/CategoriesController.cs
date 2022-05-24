using Course_API.Interfaces;
using Course_API.ViewModels.CategoryViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Course_API.Controllers
{
    [ApiController]
    [Route("api/v1/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoriesController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<CategoryViewModel>>> ListCategories() => Ok(await _categoryRepo.ListCategoriesAsync());

        [HttpPost]
        public async Task<ActionResult> AddCategoryAsync(CategoryViewModel category)
        {
            try
            {
                await _categoryRepo.AddCategoryAsync(category);

                return await _categoryRepo.SaveChangesAsync() ? StatusCode(201) : StatusCode(500, "An error occurred while saving the category to the database.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult> DeleteCategory(string name)
        {
            try
            {
                await _categoryRepo.DeleteCategoryAsync(name);

                return await _categoryRepo.SaveChangesAsync() ? NoContent() : StatusCode(500, "An error occurred while attempting to delete the category from the database");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}