using System;
using System.Data.SqlClient;

namespace MovieApplication.Helpers
{
    public class DbHelper
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CinemaDB;");
        }
    }
}