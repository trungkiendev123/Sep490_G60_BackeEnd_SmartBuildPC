using System;
using System.Collections.Generic;

namespace Sep490_G60_Backend_SmartBuildPC.Models
{
    public partial class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int WarrantyId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual ProductWarranty Warranty { get; set; } = null!;
    }
}
