using System;
using System.Collections.Generic;

namespace Sep490_G60_Backend_SmartBuildPC.Models
{
    public partial class Category
    {
        public Category()
        {
            FilterTypeCategories = new HashSet<FilterTypeCategory>();
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<FilterTypeCategory> FilterTypeCategories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
