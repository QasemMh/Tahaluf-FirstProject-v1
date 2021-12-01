using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class Slider
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
