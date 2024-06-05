using Sep490_G60_Backend_SmartBuildPC.DTOs;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public interface IProductRepository
    {
        Task<ProductDTO> GetProduct(int id);
        
    }
}
