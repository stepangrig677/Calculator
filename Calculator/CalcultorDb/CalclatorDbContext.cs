using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Calculator.CalcultorDb
{
    public class CalclatorDbContext : DbContext
    {

        public CalclatorDbContext() : base("Calculator")
        {

        }

        public DbSet<Log> Logs { get; set; }
    }
}