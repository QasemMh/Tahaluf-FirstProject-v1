using System;
using System.Collections.Generic;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class CartItem
    {
        public long Id { get; set; }
        public short Quntity { get; set; }
        public DateTime Createddate { get; set; }
        public long ProductId { get; set; }
        public long? ShoppingId { get; set; }

        public virtual Product Product { get; set; }
        public virtual ShoppingSession Shopping { get; set; }
    }
}
