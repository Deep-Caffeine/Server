namespace server.DTOs;

public class ChatLogResponse
{
    public long sender { get; set; }
    
    public long receiver { get; set; }
    
    public string message { get; set; }

    public DateTime datatime { get; set; }
}
