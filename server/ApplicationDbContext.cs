using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using server.Entities;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace server
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ChatLogsEntity> ChatLogsEntities { get; set; }
        public DbSet<ChatParticipantsEntity> ChatParticipantsEntities { get; set; }
        public DbSet<ChatRoomEntity> ChatRoomEntities { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasIndex(p => p.Email)
                .IsUnique(true);
        }
    }
}
