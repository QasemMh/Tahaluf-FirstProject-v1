using System;
using System.Collections.Generic;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class Login
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public long RoleId { get; set; }
        public long? AccountatntId { get; set; }
        public long? CustomerId { get; set; }

        public virtual Employee Accountatnt { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Role Role { get; set; }
    }
}
