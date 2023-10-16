using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.DTOs;
using server.Entities;
using server.Interface;
using server.Utilities;

namespace server.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly AuthService _authService;
    private readonly ILogger<UserService> _logger;

    public UserService(ApplicationDbContext context, AuthService auth, ILogger<UserService> logger)
    {
        _context = context;
        _authService = auth;
        _logger = logger;
    }

    private bool UserEntityExists(long id)
    {
        return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    public async Task<AuthResponse?> Create(CreateUserRequest body)
    {
        UserEntity userEntity = new UserEntity
        {
            Birth = body.Birth,
            Email = body.Email,
            Level = 0,
            Password = Password.SHA512(body.Password),
            Phone = body.Phone,
            ProfileURL = null,
            Sns = "",
            Username = body.Username,
            Nickname = body.Nickname
        };

        this._context.Users.Add(userEntity);
        await this._context.SaveChangesAsync();

        return new AuthResponse
        {
            access_token = _authService.GenerateAccessToken(userEntity.Id),
            refresh_token = _authService.GenerateRefreshToken(userEntity.Id)
        };
    }

    public async Task<GetUserResponse?> Read(long id)
    {
        UserEntity? user = await this._context.Users.FindAsync(id);

        if (user == null)
        {
            return null;
        }

        var response = new GetUserResponse
        {
            email = user.Email,
            username = user.Username,
            nickname = user.Nickname,
            phone = user.Phone,
            birth = user.Birth,
            profile_url = user.ProfileURL,
            level = user.Level,
            sns = user.Sns.Split(',')
        };
        return response;
    }

    public async Task<bool> Update(long id, PutUserRequest model)
    {
        UserEntity? user = await this._context.Users.FindAsync(id);

        if (user == null)
        {
            return false;
        }

        user.Username = model.username ?? user.Username;
        user.Nickname = model.nickname ?? user.Nickname;
        user.Password = model.password ?? user.Password;
        user.Phone = model.phone ?? user.Phone;
        user.Birth = model.birth ?? user.Birth;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task Delete(long id)
    {
        UserEntity? user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            throw new Exception("The user with that ID does not exist.");
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AddSchoolInfo(long id, CreateSchoolRequest body)
    {
        SchoolInformationEntity? data = _context.SchoolInformationEntities.SingleOrDefault(e => e.UserId == id);
        if (data == null)
        {
            SchoolInformationEntity schoolInformationEntity = new SchoolInformationEntity
            {
                UserId = id,
                School = body.School,
                Department = body.Department,
                State = body.State,
                Grade = body.Grade
            };

            this._context.SchoolInformationEntities.Add(schoolInformationEntity);
            await this._context.SaveChangesAsync();
            return true;
        }

        data.School = body.School;
        data.Department = body.Department;
        data.State = body.State;
        if (body.Grade != null) data.Grade = body.Grade;
        _context.SchoolInformationEntities.Update(data);
        await _context.SaveChangesAsync();
        return true;
    }
}
