using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class Customeraddress
    {
        public long Id { get; set; }

        [Required]
        public string Countray { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Phone { get; set; }

        public string Email { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
