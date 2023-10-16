using System.ComponentModel.DataAnnotations;

namespace server.Entities
{
    public class ChatRoomEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
