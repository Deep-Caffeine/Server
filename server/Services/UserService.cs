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
    
    public GetUserResponse Read(long id)
    {
        var user = mContext.Users.Find(id);
    
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
