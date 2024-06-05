using Microsoft.EntityFrameworkCore;
using Sep490_G60_Backend_SmartBuildPC.DTOs;
using Sep490_G60_Backend_SmartBuildPC.Models;
using Sep490_G60_Backend_SmartBuildPC.Repositories;
using System.Collections.Generic;

namespace Sep490_G60_SmartBuildPC_BE.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly SMARTPCContext _context;
        public ProductRepository(SMARTPCContext context)
        {
            _context = context;

        }
        public async Task<ProductDTO> GetProduct(int id)
        {
            try
            {
                var list = await _context.Products.Include(x => x.Category).Where(x => x.ProductId == id).Select(n => new ProductDTO { ProductId = n.ProductId,ProductName = n.ProductName }).SingleAsync() ;
                return list;
                //return list;
            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred while getting the product.", ex);
            }
        }


        

    }
}
