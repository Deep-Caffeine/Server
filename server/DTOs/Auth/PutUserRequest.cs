using System.ComponentModel.DataAnnotations;

namespace server.DTOs;

public class PutUserRequest
{
    [StringLength(20, MinimumLength = 3)]
    public string? username { get; set; }

    [StringLength(20, MinimumLength = 3)]
    public string? nickname { get; set; }

    [RegularExpression(@"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$")]
    public string? password { get; set; }

    [Phone]
    public string? phone { get; set; }
\
    [RegularExpression(@"^\d{6}$")]
    public string? birth { get; set; }
}
