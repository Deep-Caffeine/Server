using System.ComponentModel.DataAnnotations;

namespace server.Entities
{
    public class ChatLogsEntity : BaseEntity
    {
        [Required]
        public UserEntity Sender { get; set; }

        [Required]
        public UserEntity Receiver { get; set; }

        [Required]
        public string DateTime { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public ChatRoomEntity RoomId { get; set; }

        public ChatLogsEntity()
        {
            DateTime = "";
            Message = "";
        }
    }
}

