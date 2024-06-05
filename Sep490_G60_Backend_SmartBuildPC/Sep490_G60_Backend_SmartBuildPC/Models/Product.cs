using System;
using System.Collections.Generic;

namespace Sep490_G60_Backend_SmartBuildPC.Models
{
    public partial class Product
    {
        public Product()
        {
            Comments = new HashSet<Comment>();
            ProductStores = new HashSet<ProductStore>();
            ProductWarranties = new HashSet<ProductWarranty>();
        }

        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Warranty { get; set; }
        public string? Brand { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ProductStore> ProductStores { get; set; }
        public virtual ICollection<ProductWarranty> ProductWarranties { get; set; }
    }
}
