using System.ComponentModel.DataAnnotations;

namespace server.DTOs;

public class PutUserRequest
{
    [StringLength(20, MinimumLength = 3)]
    public string? username { get; set; }

    [RegularExpression(@"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
    public string? password { get; set; }

    [Phone]
    public string? phone { get; set; }

    public DateTime? birth { get; set; }
}
