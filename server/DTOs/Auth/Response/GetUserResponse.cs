namespace server.DTOs;

public class GetUserResponse
{
    public string email { get; set; }
    public string username { get; set; }
    public string nickname { get; set; }
    public string phone { get; set; }
    public string birth { get; set; }
    public string? profile_url { get; set; }
    public long level { get; set; }
    public string[] sns { get; set; }
}
