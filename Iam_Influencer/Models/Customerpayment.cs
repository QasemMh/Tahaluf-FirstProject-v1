using System;
using System.Collections.Generic;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class Customerpayment
    {
        public long Id { get; set; }
        public string AccountId { get; set; }
        public DateTime? ExpierDate { get; set; }
        public string PaymentType { get; set; }
        public string Provider { get; set; }
        public long CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
