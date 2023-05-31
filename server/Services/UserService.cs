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
    
    public GetUserResponse Read()
    {
        var response = new GetUserResponse
        {
            email = "deepcaffeine@deu.ac.kr",
            username = "폰성준",
            phone = "010-0000-0000",
            birth = "1998-02-08",
            profile_url = "/images/profile001.png",
            level = 1,
            sns = new string[] {"kakao", "naver"}
        };
        return response;
    }
}
