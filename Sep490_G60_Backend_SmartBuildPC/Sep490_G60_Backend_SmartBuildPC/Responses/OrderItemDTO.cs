using Sep490_G60_Backend_SmartBuildPC.DTOs;

namespace Sep490_G60_Backend_SmartBuildPC.Responses
{
    public class OrderItemDTO
    {
         public int OrderItemID { get; set; }
         public DateTime OrderDate { get; set; }
         public string OrderStatus { get; set; } = null!;
         public string OrderAddress { get; set; }
         public DateTime ReceiveDate { get; set; }
         public decimal TotalAmount { get; set; }
         public int Quantity { get; set; }
         public decimal PricePerItem { get; set; }
         public string ProductName { get; set; }
         public string Description { get; set; }
         public decimal Price { get; set; }
         public string Warranty { get; set; }
         public string Brand { get; set; }
         public DateTime WarrantySentDate { get; set; }
         public DateTime WarrantyReceive { get; set; }

 
         public string CustomerName { get; set; }
         public string CustomerPhone { get; set; }
    }
}
