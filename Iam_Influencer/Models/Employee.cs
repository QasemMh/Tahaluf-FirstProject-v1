using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
#nullable disable

namespace Iam_Influencer.Models
{
    public partial class Employee
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Midname { get; set; }
        public string Lastname { get; set; }
        public string Imagepath { get; set; }



        [Display(Name = "Profile Picture")]
        [NotMapped]
        public IFormFile Image { get; set; }

        public virtual Login Login { get; set; }
    }
}
