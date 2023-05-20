using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using server.DTOs;

namespace server.Interface;

interface IUserService
{
    GetUserResponse Create();
}
