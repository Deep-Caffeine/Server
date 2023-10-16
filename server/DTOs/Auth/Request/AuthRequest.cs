using System.ComponentModel.DataAnnotations;

namespace server.DTOs;

public class AuthRequest
{
    [Required]
    public string email { get; set; }

    [Required]
    public string password { get; set; }

    public bool? no_token { get; set; }
}
