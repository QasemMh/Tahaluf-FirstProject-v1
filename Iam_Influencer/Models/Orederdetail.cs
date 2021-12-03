using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class Orederdetail
    {
        public Orederdetail()
        {
            Orederitems = new HashSet<Orederitem>();
        }

        public long Id { get; set; }
        public decimal Total { get; set; }

        [DataType(DataType.Date)]
        public DateTime Createddate { get; set; }
        public long? PaymentdetailsId { get; set; }
        public long? ShippingId { get; set; }
        public long? StutascodeId { get; set; }
        public long CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Paymentdetail Paymentdetails { get; set; }
        public virtual Shipping Shipping { get; set; }
        public virtual Stutascode Stutascode { get; set; }
        public virtual ICollection<Orederitem> Orederitems { get; set; }
    }
}
