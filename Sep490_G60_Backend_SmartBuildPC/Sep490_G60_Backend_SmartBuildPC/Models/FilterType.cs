using System;
using System.Collections.Generic;

namespace Sep490_G60_Backend_SmartBuildPC.Models
{
    public partial class FilterType
    {
        public FilterType()
        {
            FilterTypeCategories = new HashSet<FilterTypeCategory>();
        }

        public int FilterTypeId { get; set; }
        public string? FilterType1 { get; set; }

        public virtual ICollection<FilterTypeCategory> FilterTypeCategories { get; set; }
    }
}
