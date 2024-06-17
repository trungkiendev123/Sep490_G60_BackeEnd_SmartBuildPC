using Microsoft.EntityFrameworkCore;
using Sep490_G60_Backend_SmartBuildPC.DTOs;
using Sep490_G60_Backend_SmartBuildPC.Models;
using Sep490_G60_Backend_SmartBuildPC.Responses;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly SMARTPCContext _context;
        public StoreRepository(SMARTPCContext context)
        {
            _context = context;

        }
        public async Task<List<StoreDTO>> ListStore()
        {
            try
            {

                var list = await _context.Stores.ToListAsync();

                return list.Select(x => new StoreDTO
                {
                    StoreID = x.StoreId,
                    Name = x.StoreName,
                    Address= x.Address
                }).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while getting the store list.", ex);
            }
        }
    }
}
