using Microsoft.EntityFrameworkCore;
using Sep490_G60_Backend_SmartBuildPC.Models;
using Sep490_G60_Backend_SmartBuildPC.Responses;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SMARTPCContext _context;

        public CategoryRepository(SMARTPCContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories
                                           .Select(c => new CategoryDTO
                                           {
                                               CategoryId = c.CategoryId,
                                               CategoryName = c.CategoryName
                                           })
                                           .ToListAsync();

            return categories;
        }




        public async Task<CategoryDTO> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _context.Categories
                                         .Where(c => c.CategoryId == categoryId)
                                         .Select(c => new CategoryDTO
                                         {
                                             CategoryId = c.CategoryId,
                                             CategoryName = c.CategoryName
                                         })
                                         .FirstOrDefaultAsync();

            if (category == null)
            {
                throw new KeyNotFoundException("Category not found.");
            }

            return category;
        }
    }
}
