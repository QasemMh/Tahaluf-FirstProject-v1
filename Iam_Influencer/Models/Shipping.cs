using System;
using System.Collections.Generic;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class Shipping
    {
        public Shipping()
        {
            Orederdetails = new HashSet<Orederdetail>();
        }

        public long Id { get; set; }
        public string Method { get; set; }
        public decimal Charge { get; set; }




        public virtual ICollection<Orederdetail> Orederdetails { get; set; }
    }
}
