namespace Sep490_G60_Backend_SmartBuildPC.Responses
{
    public class ItemCartDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImageLink { get; set; }
        public int? quantity { get; set; }
    }
}
