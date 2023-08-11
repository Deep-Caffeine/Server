using System.ComponentModel.DataAnnotations;

namespace server.Entities
{
    public class ChatLogsEntity : BaseEntity
    {
        [Required]
        public virtual UserEntity Sender { get; set; }

        [Required]
        public virtual UserEntity Receiver { get; set; }

        [Required]
        public string DateTime { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public virtual ChatRoomEntity RoomId { get; set; }

        public ChatLogsEntity()
        {
            DateTime = "";
            Message = "";
        }
    }
}

