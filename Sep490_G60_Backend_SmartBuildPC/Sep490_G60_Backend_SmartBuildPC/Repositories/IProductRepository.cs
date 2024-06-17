using Sep490_G60_Backend_SmartBuildPC.DTOs;
using Sep490_G60_Backend_SmartBuildPC.Responses;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public interface IProductRepository
    {
        Task<ProductDTO> GetProduct(int id);

        Task<List<ProductDTO>> GetProductByBrand(string brandName);
        
        Task<List<ProductDTO>> GetProductByGroup(string name);
        Task<List<ProductDTO>> GetAllProducts(int pageNumber, int pageSize);
        

        Task<IEnumerable<ProductDTO>> GetProductsByCategory(int categoryID);
        Task<List<ProductDTO>> GetProductsByKeyword(string keyword, int pageNumber = 1, int pageSize = 50);

        Task<PreviewProductDTO> PreviewProduct(string id);

    }
}
