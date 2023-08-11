using System.ComponentModel.DataAnnotations;

namespace server.Entities
{
    public class ChatParticipantsEntity
    {
        [Required]
        public virtual UserEntity UserId { get; set; }

        [Required]
        public virtual ChatRoomEntity ChatRoomId { get; set; }
    }
}
