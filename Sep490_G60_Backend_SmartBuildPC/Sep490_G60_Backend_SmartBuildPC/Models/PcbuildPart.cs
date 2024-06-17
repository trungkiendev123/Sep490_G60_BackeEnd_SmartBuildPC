using System;
using System.Collections.Generic;

namespace Sep490_G60_Backend_SmartBuildPC.Models
{
    public partial class PcbuildPart
    {
        public int? PcbuildId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Pcbuild? Pcbuild { get; set; }
        public virtual Product? Product { get; set; }
    }
}
