using System.ComponentModel.DataAnnotations;

namespace server.Entities
{
    public class ChatLogsEntity : BaseEntity
    {
        [Required]
        public long SenderId { get; set; }
        public UserEntity Sender { get; set; }

        [Required]
        public long ReceiverId { get; set; }
        public UserEntity Receiver { get; set; }

        [Required]
        public string DateTime { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public long RoomId { get; set; }
        public ChatRoomEntity Room { get; set; }
    }
}

