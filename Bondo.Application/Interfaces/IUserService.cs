using System.Security.Claims;
using Bondo.Application.DTOs;
using Bondo.Application.Models.User;
using Bondo.Shared;

namespace Bondo.Application.Interfaces;
public interface IUserService
{
    public Task<Result<GetUserDto>> RegisterAccount(RegisterRequestModel registerAccount);
    public Task<Result<GetUserDto>> Login(LoginRequestModel loginRequest);
    public Task<Result<GetUserDto>> GetUserById(string id);
    public Task<Result<GetUserDto>> GetCurrentUser(ClaimsPrincipal principal);
    public Task<Result<List<GetUserDto>>> GetUsers();
}
