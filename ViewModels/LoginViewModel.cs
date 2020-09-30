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
    public class LoginViewModel
    {

        [Required(ErrorMessage = "You must enter your email!")]
        [EmailAddress(ErrorMessage = "You must enter a VALID email! (includes @)")]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [StringLength(200, MinimumLength = 8, ErrorMessage = "Your password must be no longer than {1} characters and {2} or more characters")]
        [Required(ErrorMessage = "You must enter your password!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }


    }
}
