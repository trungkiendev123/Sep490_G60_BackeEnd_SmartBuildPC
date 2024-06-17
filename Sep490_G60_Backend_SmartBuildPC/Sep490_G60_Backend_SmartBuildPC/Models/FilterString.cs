using System;
using System.Collections.Generic;

namespace Sep490_G60_Backend_SmartBuildPC.Models
{
    public partial class FilterString
    {
        public int FilterStringId { get; set; }
        public int? FilterTypeCategoryId { get; set; }
        public string? FilterString1 { get; set; }

        public virtual FilterTypeCategory? FilterTypeCategory { get; set; }
    }
}
