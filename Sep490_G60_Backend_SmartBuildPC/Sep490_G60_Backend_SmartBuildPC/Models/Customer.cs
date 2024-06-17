using System;
using System.Collections.Generic;

namespace Sep490_G60_Backend_SmartBuildPC.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Comments = new HashSet<Comment>();
            Orders = new HashSet<Order>();
            Pcbuilds = new HashSet<Pcbuild>();
        }

        public Guid CustomerId { get; set; }
        public Guid AccountId { get; set; }
        public string Phone { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Address { get; set; } = null!;

        public virtual Account Account { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Pcbuild> Pcbuilds { get; set; }
    }
}
