using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Test_Identity.Models
{
    public class RMSDbContext : DbContext
    {
        public RMSDbContext() : base("RMS")
        {

        }
        public DbSet<Skills> skill { get; set; }
        public object Registrations { get; internal set; }
    }
}