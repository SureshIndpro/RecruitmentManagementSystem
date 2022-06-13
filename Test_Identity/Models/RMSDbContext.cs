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
        public DbSet<Job> job { get; set; }
        public DbSet<RegisterViewModel> Registrations { get; internal set; }
    }
}