using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class Slider
    {
        public long Id { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }


        [Display(Name = "Image")]
        public string Imagepath { get; set; }


        [Display(Name = "Slider Picture")]
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
