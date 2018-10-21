using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

using SportizeMvc.Models;

namespace SportizeMvc.DAL
{
    public class SportizeContext : DbContext
    {
        public SportizeContext() : base("SportizeContext")
        {}

        public DbSet<Player> Players { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}