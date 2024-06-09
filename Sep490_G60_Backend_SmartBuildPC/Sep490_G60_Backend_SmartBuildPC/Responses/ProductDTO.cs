namespace Sep490_G60_Backend_SmartBuildPC.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; } = null!;
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Warranty { get; set; }
        public string Brand { get; set; }
    }
}
