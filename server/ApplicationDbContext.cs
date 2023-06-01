using Microsoft.EntityFrameworkCore;
using server.Entities;

namespace server
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public ApplicationDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // FIXME: 이후 환경변수로 변경 필요
            string connectionString = "Server=localhost;Database=deepcaffeine;Uid=root;Pwd=toor;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
