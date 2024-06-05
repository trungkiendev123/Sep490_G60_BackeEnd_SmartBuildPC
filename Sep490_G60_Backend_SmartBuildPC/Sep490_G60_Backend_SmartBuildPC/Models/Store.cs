using System;
using System.Collections.Generic;

namespace Sep490_G60_Backend_SmartBuildPC.Models
{
    public partial class Store
    {
        public Store()
        {
            ProductStores = new HashSet<ProductStore>();
            staff = new HashSet<staff>();
        }

        public int StoreId { get; set; }
        public string StoreName { get; set; } = null!;
        public string Address { get; set; } = null!;

        public virtual ICollection<ProductStore> ProductStores { get; set; }
        public virtual ICollection<staff> staff { get; set; }
    }
}
