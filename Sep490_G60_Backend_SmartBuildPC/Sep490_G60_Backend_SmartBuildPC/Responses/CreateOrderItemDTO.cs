namespace Sep490_G60_Backend_SmartBuildPC.Responses
{
    public class CreateOrderItemDTO
    {
        public string Phone { get; set; }  // Thêm trường Phone vào DTO

        // Các trường dữ liệu khác vẫn giữ nguyên
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string OrderAddress { get; set; }
        public DateTime ReceiveDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime WarrantyStartDate { get; set; }
        public DateTime WarrantyEndDate { get; set; }
    }
}
