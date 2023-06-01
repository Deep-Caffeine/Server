namespace server.DTOs;

public class GetUserResponse
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Phone { get; set; }
    public string Birth { get; set; }
    public string? Profile_url { get; set; }
    public long Level { get; set; }
    public string[] Sns { get; set; }
}
