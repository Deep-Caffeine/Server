using System.ComponentModel.DataAnnotations;

namespace server.Entities
{
    public class ChatParticipantsEntity : BaseEntity
    {
        [Required]
        public virtual UserEntity UserId { get; set; }

        [Required]
        public virtual ChatRoomEntity ChatRoomId { get; set; }
    }
}
