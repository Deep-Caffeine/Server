using server.DTOs;
using server.Interface;

namespace server.Services;

public class UserService : IUserService
{
    public GetUserResponse Create()
    {
        return new GetUserResponse();
    }
    
}
