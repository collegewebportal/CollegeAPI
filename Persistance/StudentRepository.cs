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

        public DbSet<Student> Student { get; set; }
        public DbSet<User> User { get; set; }
    }
}
