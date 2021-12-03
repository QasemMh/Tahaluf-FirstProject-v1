using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class Product
    {
        public Product()
        {
            CartItems = new HashSet<CartItem>();
            Orederitems = new HashSet<Orederitem>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Imagepath { get; set; }
        public long? Sale { get; set; }
        public decimal Price { get; set; }
        public long? CategoryId { get; set; }
        public long CustomerId { get; set; }


        [NotMapped]
        public IFormFile Image { get; set; }

        public virtual Category Category { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<Orederitem> Orederitems { get; set; }
    }
}
