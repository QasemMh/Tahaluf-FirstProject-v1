using System;
using System.Collections.Generic;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class ShoppingSession
    {
        public ShoppingSession()
        {
            CartItems = new HashSet<CartItem>();
        }

        public long Id { get; set; }
        public decimal Total { get; set; }
        public DateTime Createddate { get; set; }
        public long CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
