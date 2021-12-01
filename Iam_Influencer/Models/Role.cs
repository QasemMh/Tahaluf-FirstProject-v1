using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class Role
    {
        public Role()
        {
            Logins = new HashSet<Login>();
        }

        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Login> Logins { get; set; }
    }
}
