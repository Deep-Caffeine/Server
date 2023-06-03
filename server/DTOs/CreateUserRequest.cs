using System.ComponentModel.DataAnnotations;

namespace server.DTOs;

public class CreateUserRequest
{
    [EmailAddress]
    public string Email { get; set; }

    [RegularExpression(@"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
    public string Password { get; set; }

    [StringLength(20, MinimumLength = 3)]
    public string Username { get; set; }

    [Phone]
    public string Phone { get; set; }

    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
    public string Birth { get; set; }
}
