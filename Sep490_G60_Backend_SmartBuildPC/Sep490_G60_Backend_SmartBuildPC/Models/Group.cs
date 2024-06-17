using System;
using System.Collections.Generic;

namespace Sep490_G60_Backend_SmartBuildPC.Models
{
    public partial class Group
    {
        public int PcbuildId { get; set; }
        public string? Pctype { get; set; }

        public virtual Pcbuild Pcbuild { get; set; } = null!;
    }
}
