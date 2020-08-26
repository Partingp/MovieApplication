using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using MovieApplication.Models;
using Dapper;
using MovieApplication.Helpers;

namespace MovieApplication.ViewModels
{
    public class MoviesViewModel
    {
        //Returns a list of movies from the movies table
        public MoviesViewModel()
        {
            using (var db = DbHelper.GetConnection())
            {
                this.TodoItems = db.Query<MovieItem>("SELECT * FROM Movies ORDER BY AddDate DESC").ToList();
            }
        }

        public List<MovieItem> TodoItems { get; set; }
    }
}

