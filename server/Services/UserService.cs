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

    public GetUserResponse Create()
    {
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

    public async Task<PutUserRequest?> Update(long id, PutUserRequest model)
    {
        UserEntity? user = await this.mContext.Users.FindAsync(id);

        if (user == null)
        {
            return null;
        }

        user.Username = model.username ?? user.Username;
        user.Password = model.password ?? user.Password; 
        user.Phone = model.phone ?? user.Phone;
        user.Birth = model.birth ?? user.Birth;

        await mContext.SaveChangesAsync();

        return model;
    }
}
