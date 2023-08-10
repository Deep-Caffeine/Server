using System.ComponentModel.DataAnnotations;

namespace server.Entities
{
    public class ChatParticipantsEntity
    {
        [Required]
        public virtual BaseEntity UserId { get; set; }

        [Required]
        public virtual BaseChatRoomEntity ChatRoomId { get; set; }
    }
}
