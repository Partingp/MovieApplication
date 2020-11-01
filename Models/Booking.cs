using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MovieApplication.Helpers;

namespace MovieApplication.Models
{
    public class Booking
    {
        
        public int BookingId { get; } 
        
        public int MovieTimeTableId { get; set; }
        public int ApplicationUserId { get; set; }

        public string Seat { get; set; }

        public void setBooking(string[] seats)
        {   
            using (var db = DbHelper.GetConnection())
            {

                //Check for duplicates entries
                string sql = "INSERT INTO Bookings (MovieTimeTableId,ApplicationUserId,Seat)" +
                            " VALUES (@MovieTimeTableId,@ApplicationUserId,@Seat)";
                var parameters = new List<Booking>();

                foreach(var seat in seats)
                {
                    parameters.Add(new Booking { MovieTimeTableId = this.MovieTimeTableId, ApplicationUserId = this.ApplicationUserId, Seat = seat });
                }

                var sqlStatement = db.Execute(sql, parameters); 

            };
        }

        public List<string> getTakenSeats()
        {

            using (var db = DbHelper.GetConnection())
            {
                string sql = "SELECT Seat FROM Bookings " +
                             "WHERE MovieTimeTableId = @MovieTimeTableId";
                var parameters = new { MovieTimeTableId = this.MovieTimeTableId };
                return db.Query<Booking>(sql, parameters).Select(x=>x.Seat).ToList();
            }

        }
        


    }
}
