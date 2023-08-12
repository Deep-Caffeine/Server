using Microsoft.EntityFrameworkCore;
using server.Entities;

namespace server
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ChatLogsEntity> ChatLogsEntities { get; set; }
        public DbSet<ChatParticipantsEntity> ChatParticipantsEntities { get; set; }
        public DbSet<ChatRoomEntity> ChatRoomEntities { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
