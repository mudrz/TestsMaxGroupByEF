using Microsoft.EntityFrameworkCore;

namespace MaxTest.Models
{
    public class LiteContext : DbContext
    {
        public LiteContext (DbContextOptions<LiteContext> options)
            : base(options)
        {
        }

        public DbSet<Parent> Parents { get; set; }
        public DbSet<Child> Children { get; set; }
    }
}