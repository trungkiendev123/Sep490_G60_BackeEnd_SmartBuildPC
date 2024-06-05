using System;
using System.Collections.Generic;

namespace Sep490_G60_Backend_SmartBuildPC.Models
{
    public partial class Bill
    {
        public int BillId { get; set; }
        public int? OrderId { get; set; }
        public DateTime? BillDate { get; set; }
        public int? TaxIn { get; set; }
        public string? Address { get; set; }

        public virtual Order? Order { get; set; }
    }
}
