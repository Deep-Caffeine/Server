using System.ComponentModel.DataAnnotations;

namespace server.DTOs;

public class ChatJoinRequest
{
    [Required]
    public long roomid { get; set; }
}
