using System.ComponentModel.DataAnnotations;
using Microsoft.Build.Framework;

namespace server.DTOs;

public class PutUserRequest
{
    [StringLength(20, MinimumLength = 3)]
    public string? username { get; set; }
    [RegularExpression(@"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
    public string? password { get; set; }
    [Phone]
    public string? phone { get; set; }
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
    public string? birth { get; set; }
}
