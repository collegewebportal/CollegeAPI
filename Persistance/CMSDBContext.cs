using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistance
{
    public class CMSDBContext : DbContext
    {
        public CMSDBContext(DbContextOptions<CMSDBContext> options)
            : base(options)
        { }

        
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Leads> Leads { get; set; }
        public DbSet<Source> Sources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
