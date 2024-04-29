using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Question2.Models
{
    public class Movies
    {
        [Key]
        public int Movie_id { get; set; }
        public string Movie_Name { get; set; }
        public DateTime DateofRelease { get; set; }
    }
}