using Microsoft.EntityFrameworkCore;
using Sep490_G60_Backend_SmartBuildPC.Models;
using Sep490_G60_Backend_SmartBuildPC.Responses;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public class BuildPCRepository : IBuildPCRepository
    {
        private readonly SMARTPCContext _context;
        public BuildPCRepository (SMARTPCContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CategoryDTO>> listCategory()
        {
            try
            {
                var list = await _context.Categories.ToListAsync();
                return  list.Select(x => new CategoryDTO
                {
                    CategoryName= x.CategoryName,
                    CategoryId= x.CategoryId
                });

            }catch(Exception ex)
            {
                throw new Exception("An error occurred while do this action", ex);
            }
        }
    }
}
