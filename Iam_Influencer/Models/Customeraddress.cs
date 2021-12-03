using System;
using System.Collections.Generic;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class Customeraddress
    {
        public Customeraddress()
        {
            Customers = new HashSet<Customer>();
        }

        public long Id { get; set; }
        public string Countray { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
