namespace server.DTOs;

public class PutUserRequest
{
    public string? username { get; set; }
    public string? password { get; set; }
    public string? phone { get; set; }
    public string? birth { get; set; }
}
