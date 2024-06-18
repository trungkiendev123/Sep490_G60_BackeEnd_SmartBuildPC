using System;
using System.Collections.Generic;

namespace Sep490_G60_Backend_SmartBuildPC.Models
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
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
        public string? Tag { get; set; }
        public int? Tdp { get; set; }
        public string? ImageLink { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ProductStore> ProductStores { get; set; }
        public virtual ICollection<ProductWarranty> ProductWarranties { get; set; }
    }
}
