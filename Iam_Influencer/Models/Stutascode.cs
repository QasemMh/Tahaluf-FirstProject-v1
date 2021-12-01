using System;
using System.Collections.Generic;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class Stutascode
    {
        public Stutascode()
        {
            Orederdetails = new HashSet<Orederdetail>();
        }

        public long Id { get; set; }
        public string Stutas { get; set; }

        public virtual ICollection<Orederdetail> Orederdetails { get; set; }
    }
}
