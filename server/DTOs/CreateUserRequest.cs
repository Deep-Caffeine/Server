using System.ComponentModel.DataAnnotations;

namespace server.DTOs;

public class CreateUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
    public string Phone { get; set; }
    public string Birth { get; set; }
}
