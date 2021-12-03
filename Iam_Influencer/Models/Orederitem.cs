using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class Orederitem
    {
        public long Id { get; set; }
        public short Quntity { get; set; }

        [DataType(DataType.Date)]

        public DateTime Createddate { get; set; }
        public long ProductId { get; set; }
        public long OrederdetailsId { get; set; }

        public virtual Orederdetail Orederdetails { get; set; }
        public virtual Product Product { get; set; }
    }
}
