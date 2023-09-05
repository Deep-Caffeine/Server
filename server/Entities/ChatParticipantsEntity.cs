using System.ComponentModel.DataAnnotations;

namespace server.Entities
{
    public class ChatParticipantsEntity : BaseEntity
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public long ChatRoomId { get; set; }
    }
}
