using server.DTOs;

namespace server.Interface;

interface IUserService
{
    public Task<AuthResponse> Create(CreateUserRequest body);
    public Task<bool> AddSchoolInfo(long id, CreateSchoolRequest body);

}

