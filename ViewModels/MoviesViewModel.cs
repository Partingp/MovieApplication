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
                this.MovieItems = db.Query<MovieItem>("SELECT * FROM Movies ORDER BY ReleaseDate DESC").ToList();
            }
        }

        public void getFilteredMovies(string filters)
        {
            using (var db = DbHelper.GetConnection())
            {
                string sql = "SELECT DISTINCT Title,ReleaseDate,Runtime,Rating,Poster FROM MovieTags as mt " +
                             "INNER JOIN Movies AS m ON (mt.MovieId=m.MovieId)" +
                             "INNER JOIN Tags AS t ON (mt.TagId=t.TagId)" +
                             "WHERE t.Tag IN @Filters " +
                             "ORDER BY ReleaseDate DESC";
                var parameters = new {Filters = filters.Split(',') };
                this.MovieItems = db.Query<MovieItem>(sql, parameters).ToList();
            }
        }
        public List<MovieItem> MovieItems { get; set; }
    }
}

