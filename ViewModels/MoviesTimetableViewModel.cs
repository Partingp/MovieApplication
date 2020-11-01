using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using MovieApplication.Models;
using Dapper;
using MovieApplication.Helpers;

namespace MovieApplication.ViewModels
{
    public class MoviesTimetableViewModel
    {

        public DateTime DateTime { get; set; }

        public int Screen { get; set; }

        public List<MoviesTimetableViewModel> getTimetable(string title)
        {
            using (var db = DbHelper.GetConnection())
            {
                string sql = "SELECT mt.DateTime,mt.Screen FROM MovieTimetable as mt " +
                             "INNER JOIN Movies AS m ON (mt.MovieId=m.MovieId)" +
                             "WHERE m.title = @Title ORDER BY DateTime ASC";
                var parameters = new {Title = title};
                return db.Query<MoviesTimetableViewModel>(sql, parameters).ToList();
            }
        }

        public List<string> getTags(string title)
        {
            using (var db = DbHelper.GetConnection())
            {
                string sql = "SELECT t.tag FROM MovieTags as mt " +
                        "INNER JOIN Movies AS m ON (mt.MovieId=m.MovieId)" +
                        "INNER JOIN Tags AS t ON (mt.TagId=t.TagId)" +
                        "WHERE m.title = @Title AND t.Genre = @Genre";
                var parameters = new { Title = title, Genre=0 };
                return db.Query<string>(sql, parameters).ToList();
            }
        }
    }
}

