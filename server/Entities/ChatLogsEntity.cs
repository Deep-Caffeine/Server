using System.ComponentModel.DataAnnotations;

namespace server.Entities
{
    public class ChatLogsEntity : BaseEntity
    {
        [Required]
        public long Sender { get; set; }

        [Required]
        public long Receiver { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public long RoomId { get; set; }

        public ChatLogsEntity()
        {
            Message = "";
        }
    }
}

