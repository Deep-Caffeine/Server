using System.ComponentModel.DataAnnotations;

namespace server.Entities
{
    public class ChatParticipantsEntity : BaseEntity
    {
        [Required]
        public UserEntity UserId { get; set; }

        [Required]
        public ChatRoomEntity ChatRoomId { get; set; }
    }
}
