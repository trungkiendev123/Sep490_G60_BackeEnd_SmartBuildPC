using System;
using System.Collections.Generic;

namespace Sep490_G60_Backend_SmartBuildPC.Models
{
    public partial class FilterTypeCategory
    {
        public FilterTypeCategory()
        {
            FilterStrings = new HashSet<FilterString>();
        }

        public int FilterTypeCategoryId { get; set; }
        public int? FilterTypeId { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual FilterType? FilterType { get; set; }
        public virtual ICollection<FilterString> FilterStrings { get; set; }
    }
}
