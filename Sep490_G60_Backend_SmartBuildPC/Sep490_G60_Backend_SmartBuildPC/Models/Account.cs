using System;
using System.Collections.Generic;

namespace Sep490_G60_Backend_SmartBuildPC.Models
{
    public partial class Account
    {
        public Account()
        {
            Customers = new HashSet<Customer>();
            staff = new HashSet<staff>();
        }

        public Guid AccountId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string AccountType { get; set; } = null!;
        public int? Status { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<staff> staff { get; set; }
    }
}
