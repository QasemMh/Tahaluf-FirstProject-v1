using System;
using System.Collections.Generic;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class Paymentdetail
    {
        public Paymentdetail()
        {
            Orederdetails = new HashSet<Orederdetail>();
        }

        public long Id { get; set; }
        public string AccountId { get; set; }
        public DateTime Createddate { get; set; }
        public DateTime ExpierDate { get; set; }
        public string PaymentType { get; set; }
        public string Provider { get; set; }
        public decimal Amount { get; set; }

        public virtual ICollection<Orederdetail> Orederdetails { get; set; }
    }
}
