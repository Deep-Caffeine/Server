using server.DTOs;

namespace server.Interface;

interface IUserService
{
    public Task<AuthResponse?> Create(CreateUserRequest body);

}
