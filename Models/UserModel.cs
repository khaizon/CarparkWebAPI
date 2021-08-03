using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CarparkAPIApp.Models
{
    public class UserModel : IdentityUser
    {
        [Required]
        public override string Email {get; set;}
        [Required]
        public string FirstName {get; set;}
        [Required]
        public string LastName {get; set;}

        public int ContactNumber {get; set;}
    }
}
