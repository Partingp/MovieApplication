using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApplication.Models
{
    public class ApplicationUser
    {
        [Key]
        public int Id { get; set; } 
        
        [Required]
        [StringLength(200, MinimumLength = 3)]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3)]
        [Display(Name = "Surname:")]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required]
        [EmailAddress]
        public string NormalisedEmail { get; set; }

        [Required]
        [Display(Name = "Password:")]
        public string PasswordHash { get; set; }

        [Required]
        [Display(Name = "Date of Birth:")]
        public DateTime DateOfBirth { get; set; }



    }
}
