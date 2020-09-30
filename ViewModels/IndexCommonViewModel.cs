using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using MovieApplication.Models;
using Dapper;
using MovieApplication.Helpers;

namespace MovieApplication.ViewModels
{
    public class IndexCommonViewModel
    {
        public MoviesViewModel MoviesVM { get; set; }

        public RegisterViewModel RegisterVM { get; set; }

        public LoginViewModel LoginVM { get; set; }

    }
}

