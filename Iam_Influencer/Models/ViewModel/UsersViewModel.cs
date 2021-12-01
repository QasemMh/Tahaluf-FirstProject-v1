using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Iam_Influencer.Models.ViewModel
{
    public class UsersViewModel
    {
        public long Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }

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

        public string RoleName { get; set; }

        public string ImagePath { get; set; }

        [Display(Name = "Profile Picture")]
        [NotMapped]
        public IFormFile Image { get; set; }

    }
}
