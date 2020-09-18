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
    public class RegisterViewModel
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

        public int addClient()
        {
            var result = 0;
            
            using (var db = DbHelper.GetConnection())
            {
                //Check for duplicates entries
                string sql = "SELECT * FROM Clients WHERE Email = @Email";
                var parameters = new { Email = Email };
                var duplicates = db.Query<Client>(sql, parameters).ToList();

                if(duplicates.Count==0)
                {
                    //Insert new client information
                    string sql2 = "INSERT INTO Clients(FirstName,Surname,Email,Password,DateOfBirth) VALUES(@FirstName,@Surname,@Email,@Password,@DateOfBirth)";
                    var insertParameters = new
                    {
                        FirstName = FirstName,
                        Surname = Surname,
                        Email = Email,
                        Password = Password,
                        DateOfBirth = DateOfBirth
                    };
                    result = db.Execute(sql2, insertParameters);
                }
            }

            return result;
        }

    }
}
