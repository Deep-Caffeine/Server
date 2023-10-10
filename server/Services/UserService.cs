using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using server.DTOs;
using server.Entities;
using server.Interface;
using server.Utilities;

namespace server.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    private bool UserEntityExists(long id)
    {
        return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    public async Task<bool> Create(CreateUserRequest body)
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
            Username = body.Username
        };
        this._context.Users.Add(userEntity);
        await this._context.SaveChangesAsync();
        return true;
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
    public async Task<bool> AddSchoolInfo(CreateSchoolRequest body)
    {
        /*
         * 도훈 선배와 얘기한 후 프론트에 전달 필요
         * 회원가입할 때 학교 정보를 입력할 때는 유저에게 토큰이 없음
         * 1. 회원가입시 로그인 처리를 하여 JWT 토큰을 발급 받는다 -> JWT를 프론트에서 전달 받음       << 확정!
         * 2. 회원가입 create가 끝난 후 고유 번호(id)를 던져준다 -> 고유 번호를 전달 받음 -> 고유 번호 if로 무결성 확인 필수
         *
         * 회원가입 create하고 프론트에서 로그인을 내부적으로 시행
         */
        
        SchoolInformationEntity schoolInformationEntity = new SchoolInformationEntity
        {
            School = body.School,
            Department = body.Department,
            State = body.State,
            Grade = body.Grade
        };
        
        this._context.SchoolInformationEntities.Add(schoolInformationEntity);
        await this._context.SaveChangesAsync();
        return true;
    }
}
