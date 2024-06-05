using System;
using System.Collections.Generic;

namespace Sep490_G60_Backend_SmartBuildPC.Models
{
    public partial class ProductStore
    {
        public int ProductStoreId { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int StockQuantity { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
    }
}
