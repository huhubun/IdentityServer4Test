using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Admin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Admin
{
    public class MyContext : IdentityDbContext<IdentityServerUser>
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Test>().HasKey(t => t.Id);
            builder.Entity<Sample>().HasKey(t => t.Id);
        }

        public DbSet<Test> Tests { get; set; }

        public DbSet<Sample> Samples { get; set; }
    }
}
