using server.DTOs;
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

    public async Task<GetUserResponse> Read(long id)
    {
        var user = await this.mContext.Users.FindAsync(id);

        if (user == null)
        {
            return new GetUserResponse();
        }
        
        var response = new GetUserResponse
        {
            Email = user.Email,
            Username = user.Username,
            Phone = user.Phone,
            Birth = user.Birth,
            Profile_url = user.ProfileURL,
            Level = user.Level,
            Sns = user.Sns.Split(',')
        };
        return response;
    }
}
