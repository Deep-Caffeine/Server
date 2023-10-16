using System.ComponentModel.DataAnnotations;

namespace server.Entities
{
    public class ChatParticipantsEntity : BaseEntity
    {
        [Required]
        public long UserId { get; set; }
        public UserEntity User { get; set; }

        [Required]
        public long ChatRoomId { get; set; }
        public ChatRoomEntity ChatRoom { get; set; }
    }
}
