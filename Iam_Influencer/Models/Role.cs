using System;
using System.Collections.Generic;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class Role
    {
        public Role()
        {
            Logins = new HashSet<Login>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Login> Logins { get; set; }
    }
}
