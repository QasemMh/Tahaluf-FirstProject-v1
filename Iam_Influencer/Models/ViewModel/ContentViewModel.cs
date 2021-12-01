using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iam_Influencer.Models.ViewModel
{
    public class ContentViewModel
    {
        public long SliderId { get; set; }
        public string SliderTitle { get; set; }
        public string SliderText { get; set; }

        //

        public long Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public bool? Starcount { get; set; }
        public string Imagepath { get; set; }
    }
}
