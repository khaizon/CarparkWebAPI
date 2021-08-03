using System;
using System.ComponentModel.DataAnnotations;

namespace CarparkAPIApp.Models.DTOs.Requests
{
    public class UserRegistrationDto
    {
        [Required]
        [EmailAddress]
        public string Email {get; set;}
        [Required]
        public string FirstName {get; set;}
        [Required]
        public string LastName {get; set;}
        [Required]
        public string Password { get; set;}
        
        public int ContactNumber {get; set;}
    }
}
