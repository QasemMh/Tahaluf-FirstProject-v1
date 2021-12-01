using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Iam_Influencer.Models.ViewModel
{
    public class EmployeeViewModel
    {
        public long Id { get; set; }

        [Required]
        public string Username { get; set; }


        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string FullName
        {
            get { return $"{this.FirstName} {this.LastName}"; }
            set { this.FullName = value; }
        }
        public string Email { get; set; }



    }
}
