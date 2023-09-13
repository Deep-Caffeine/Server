// 추후에 개발하다가 안쓰는게 확실해지면 지울 예정입니다.
namespace server.DTOs;

public class ChatLogRequest
{
    public long id { get; set; }
    
    public long room_id { get; set; }
}
