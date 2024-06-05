using System;
using System.Collections.Generic;

namespace Sep490_G60_Backend_SmartBuildPC.Models
{
    public partial class staff
    {
        public staff()
        {
            Comments = new HashSet<Comment>();
        }

        public int StaffId { get; set; }
        public int AccountId { get; set; }
        public int? StoreId { get; set; }
        public string FullName { get; set; } = null!;

        public virtual Store? Store { get; set; }
        public virtual Account? Account { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
