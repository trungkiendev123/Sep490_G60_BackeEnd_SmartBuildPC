using System;
using System.Collections.Generic;

namespace Sep490_G60_Backend_SmartBuildPC.Models
{
    public partial class Pcbuild
    {
        public int PcbuildId { get; set; }
        public Guid? CustomerId { get; set; }
        public DateTime BuildDate { get; set; }

        public virtual Customer? Customer { get; set; }
    }
}
