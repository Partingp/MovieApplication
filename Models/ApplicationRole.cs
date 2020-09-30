using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApplication.Models
{
    public class ApplicationRole
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NormalisedName { get; set; }
    }
}
