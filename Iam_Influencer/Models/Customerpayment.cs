using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class Customerpayment
    {
        public long Id { get; set; }


        public double Balance { get; set; }

        public string AccountId { get; set; }
 

        [DataType(DataType.Date)]
        public DateTime? ExpierDate { get; set; }
 
        public string PaymentType { get; set; }
        public string Provider { get; set; }
        public long CustomerId { get; set; }
        public decimal Balance { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
