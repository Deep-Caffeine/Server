using Microsoft.EntityFrameworkCore;
using server.Entities;

namespace server
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
