using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using MovieApplication.Models;
using Dapper;
using MovieApplication.Helpers;
using System.ComponentModel.DataAnnotations;

namespace MovieApplication.ViewModels
{
    public class RegisterViewModel : IValidatableObject
    {

        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Your first name must be no longer than {1} characters and {2} or more characters")]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Your surname must be no longer than {1} characters and {2} or more characters")]
        [Display(Name = "Surname:")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "You must enter your email!")]
        [EmailAddress(ErrorMessage = "You must enter a VALID email! (includes @)")]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must enter your email again!")]
        [EmailAddress(ErrorMessage = "You must enter a VALID email! (includes @)")]
        [Display(Name = "Email:")]
        public string ConfirmEmail { get; set; }

        [StringLength(200, MinimumLength = 8, ErrorMessage = "Your password must be no longer than {1} characters and {2} or more characters")]
        [Required(ErrorMessage = "You must enter your password!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [Required(ErrorMessage = "You must enter your date of birth!")]
        [Display(Name = "Date of Birth:")]
        public DateTime DateOfBirth { get; set; }

        public int CheckClientExists()
        {
            using (var db = DbHelper.GetConnection())
            {
                //Check for duplicates entries
                string sql = "SELECT * FROM ApplicationUser WHERE Email = @Email";
                var parameters = new { Email = Email };
                var duplicates = db.Query<ApplicationUser>(sql, parameters).ToList();
                return duplicates.Count;
            }

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //DateOfBirth Validation
            if (this.DateOfBirth.Year >= DateTime.Now.Year)
                yield return new ValidationResult("I dont think you were born recently or from the future", new[] { "DateOfBirth" });
            if (this.DateOfBirth.Year < (DateTime.Now.Year-150))
                yield return new ValidationResult("I dont think you are THAT old", new[] { "DateOfBirth" });
            //Email Validation
            if (!this.Email.Equals(this.ConfirmEmail))
                yield return new ValidationResult("Make sure the emails match", new[] { "ConfirmEmail" });
            if(CheckClientExists()>0)
                yield return new ValidationResult("An account already exists with this email", new[] { "Email" });
        }
    }
}
