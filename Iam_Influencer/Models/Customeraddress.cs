using System;
using System.Collections.Generic;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class Customeraddress
    {
        public long Id { get; set; }
        public string Countray { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
