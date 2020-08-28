using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApplication.Models
{
    public class MovieItem
    {
        
        public int Id { get; } 
        
        public string Title { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public int Runtime { get; set; }

        public enum Rating  { 
            Universal,
            ParentalGuidance,
            TwelveAdvisory,
            Twelve,
            Fifteen,
            Adults        
        };

        public string Poster { get; set; }

    }
}
