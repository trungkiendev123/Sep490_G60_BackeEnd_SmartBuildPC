using Sep490_G60_Backend_SmartBuildPC.Models;
using System.Numerics;

namespace Sep490_G60_Backend_SmartBuildPC.Responses
{
    public class PreviewProductDTO
    {
        public decimal price { get; set; }

        public string DetailProduct { get; set; }

        public string Warranty { get; set; }

        public IEnumerable<StoreDTO> AvaibleStore { get; set; }

    }
}