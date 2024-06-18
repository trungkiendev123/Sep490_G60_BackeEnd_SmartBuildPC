using Sep490_G60_Backend_SmartBuildPC.DTOs;

namespace Sep490_G60_Backend_SmartBuildPC.Responses
{
    public class ProductDetailsDTO
    {
        public ProductDTO Product { get; set; }
        public List<ProductDTO> SimilarPriceProducts { get; set; }
    }
}
