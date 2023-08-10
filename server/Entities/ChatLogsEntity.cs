using System.ComponentModel.DataAnnotations;

namespace server.Entities{
    public class ChatLogsEntity : BaseChatLogsEntity
    {
        [Required]
        public virtual BaseEntity sender { get; set; }
        
        [Required]
        public virtual BaseEntity receiver { get; set; }
        
        [Required]
        public string DateTime { get; set; }
        
        [Required]
        public string Message { get; set; }
        
        [Required]
        public virtual BaseChatRoomEntity RoomId { get; set; }

        public ChatLogsEntity()
        {
            DateTime = "";
            Message = "";
        }
    }
}

