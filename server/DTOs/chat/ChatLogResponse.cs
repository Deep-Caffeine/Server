namespace server.DTOs;

public class ChatLogResponse
{
    public long sender { get; set; }

    public string message { get; set; }

    public DateTime datetime { get; set; }
}
