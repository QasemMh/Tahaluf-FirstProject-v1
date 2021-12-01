using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Customerpayments = new HashSet<Customerpayment>();
            Products = new HashSet<Product>();
            ShoppingSessions = new HashSet<ShoppingSession>();
        }

        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Midname { get; set; }
        public string Lastname { get; set; }
        public string Imagepath { get; set; }
        public long? AddressId { get; set; }

        public virtual Customeraddress Address { get; set; }
        public virtual Login Login { get; set; }
        public virtual ICollection<Customerpayment> Customerpayments { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ShoppingSession> ShoppingSessions { get; set; }

        [Display(Name = "Profile Picture")]
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
