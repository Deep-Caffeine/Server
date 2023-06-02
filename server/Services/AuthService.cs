using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using server.Entities;

namespace server.Services;

public class AuthService
{
    private readonly ApplicationDbContext mContext;
    private static readonly string SECRET;

    static AuthService()
    {
        SECRET = Utilities.Password.GenerateRandomPassword(2048);
    }

    public AuthService(ApplicationDbContext context)
    {
        mContext = context;
    }

    public UserEntity? UserAuthorize(string email, string password)
    {
        UserEntity userEntity = new UserEntity();
        userEntity.Email = email;
        userEntity.Password = password;

        UserEntity[] results = mContext.Users.Where(
            userEntity => (userEntity.Email == email) && (userEntity.Password == password)
        ).ToArray();

        return results.Length == 0 ? null : results[0];
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

