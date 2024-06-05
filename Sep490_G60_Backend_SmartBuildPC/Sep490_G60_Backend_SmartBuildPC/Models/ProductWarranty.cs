using System;
using System.Collections.Generic;

namespace Sep490_G60_Backend_SmartBuildPC.Models
{
    public partial class ProductWarranty
    {
        public ProductWarranty()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int WarrantyId { get; set; }
        public int? ProductId { get; set; }
        public DateTime? WarrantySentDate { get; set; }
        public DateTime? WarrantyReceive { get; set; }

        public virtual Product? Product { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
