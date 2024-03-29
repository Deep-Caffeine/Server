﻿using System.ComponentModel.DataAnnotations;

namespace server.DTOs;

public class CreateUserRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$")]
    public string Password { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 3)]
    public string Nickname { get; set; }

    [Required]
    [Phone]
    public string Phone { get; set; }

    [Required]
    [RegularExpression(@"^\d{6}$")]
    public String Birth { get; set; }

    [Required]
    public string Gender { get; set; }
}
