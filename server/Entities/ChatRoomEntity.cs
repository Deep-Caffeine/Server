using System.ComponentModel.DataAnnotations;

namespace server.Entities
{
    public class ChatRoomEntity : BaseChatRoomEntity
    {
        [Required]
        public string Name { get; set; }

        public ChatRoomEntity()
        {
            Name = "";
        }
    }
}
