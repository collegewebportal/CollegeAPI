using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Persistance
{
    public class CMSDBContext : DbContext
    {
        public CMSDBContext(DbContextOptions<CMSDBContext> options)
            : base(options)
        { }


        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Source> Sources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }

}
