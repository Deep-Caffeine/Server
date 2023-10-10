using server.DTOs;

namespace server.Interface;

interface IUserService
{
    public Task<bool> Create(CreateUserRequest body);
    public Task<bool> SchoolInformation(CreateSchoolRequest body);

}
