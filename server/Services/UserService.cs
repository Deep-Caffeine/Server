using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using server.DTOs;
using server.Entities;
using server.Interface;

namespace server.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext mContext;

    public UserService(ApplicationDbContext context)
    {
        mContext = context;
    }

    private bool UserEntityExists(long id)
    {
        return (mContext.Users?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    public async Task<GetUserResponse?> Create(CreateUserRequest body)
    {
        //이메일 중복 조회
        List<string> user = await this.mContext.Users
            .Where(x => x.Email == body.Email)
            .Select(x => x.Email)
            .ToListAsync();
        if (user.Count == 0)
        {
            return null;
        }

        //비밀번호 암호화
        byte[] buffer = Encoding.Default.GetBytes(body.Password);
        byte[] hashBuffer;
        string hashed = "";
        using (SHA256 sha256 = SHA256.Create())
        {
            hashBuffer = sha256.ComputeHash(buffer);
        }

        foreach (var temp in hashBuffer)
        {
            hashed += temp.ToString("X2");
        }
        
        return new GetUserResponse();
    }

    public async Task<GetUserResponse?> Read(long id)
    {
        UserEntity? user = await this.mContext.Users.FindAsync(id);

        if (user == null)
        {
            return null;
        }

        var response = new GetUserResponse
        {
            email = user.Email,
            username = user.Username,
            phone = user.Phone,
            birth = user.Birth,
            profile_url = user.ProfileURL,
            level = user.Level,
            sns = user.Sns.Split(',')
        };
        return response;
    }
}
