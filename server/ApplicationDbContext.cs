using Microsoft.EntityFrameworkCore;
using server.Entities;

namespace server
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public string DbPath { get; }

        public ApplicationDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);

            DbPath = System.IO.Path.Join(path, "./data.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // 로컬 Db와 연결
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }
}
