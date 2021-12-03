using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class Review
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public int? Starcount { get; set; }
        public string Imagepath { get; set; }

        [Display(Name = "Profile Picture")]
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
