using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Question2.Models;
using System.Data.Entity;

namespace Question2.Models
{
    public class MoviesDB : DbContext
    {
        public MoviesDB() : base("Moviconn")
        { }
        public DbSet<Movies> Movies { get; set; }
    }
}