﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using server.DTOs;
using server.Entities;
using server.Utilities;

namespace server.Services;

public class AuthService
{
    private readonly ApplicationDbContext _context;
    private static readonly string SECRET;

    static AuthService()
    {
        SECRET = Utilities.Password.GenerateRandomPassword(2048);
    }

    public AuthService(ApplicationDbContext context)
    {
        _context = context;
    }

    public UserEntity? UserAuthorize(AuthRequest body)
    {
        try
        {
            UserEntity? user = this._context.Users.Single(u => u.Email == body.email && u.Password == Password.SHA512(body.password));
            return user;
        }
        catch (InvalidOperationException e)
        {
            return null;
        }
    }

    public string GenerateAccessToken(long userId)
    {
        return generateJwtToken(new[] {
            new Claim("id", userId.ToString())
        }, TimeSpan.FromMinutes(1));                 // Expires: 1 Minutes
    }

    public string GenerateRefreshToken(long userId)
    {
        return generateJwtToken(new[] {
            new Claim("id", userId.ToString()),
            new Claim("refresh_only", "true")
        }, TimeSpan.FromDays(7));                    // Expires: 7 Days
    }

    public JwtSecurityToken? ValidateToken(string token)
    {
        if (token == null)
        {
            return null;
        }

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(SECRET);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
        catch
        {
            // Validation failed
            return null;
        }
    }

    private string generateJwtToken(Claim[] claims, TimeSpan expires)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        byte[] key = Encoding.ASCII.GetBytes(SECRET);

        SigningCredentials signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature
        );

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow + expires,
            SigningCredentials = signingCredentials
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

