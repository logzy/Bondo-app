using System.Security.Claims;
using Bondo.Application.DTOs;
using Bondo.Application.Models.User;
using Bondo.Shared;

namespace Bondo.Application.Interfaces;
public interface IUserService
{
    public Task<Result<GetUserDto>> RegisterAccount(RegisterRequestModel registerAccount);
    public Task<Result<GetUserDto>> UpdateAccount(UpdateProfileRequestModel updateProfileRequest, string userId);
    public Task<Result<GetUserDto>> Login(LoginRequestModel loginRequest);
    public Task<Result<GetUserDto>> GetUserById(string id);
    public Task<Result<GetUserDto>> GetCurrentUser(ClaimsPrincipal principal);
    public Task<Result<List<GetUserDto>>> GetUsers();
    public Task<Result<bool>> RequestPasswordReset(ForgotPasswordRequestModel requestModel, string origin);
    public Task<Result<GetUserDto>> ResetPassword(PasswordResetRequestModel passwordResetRequest);
    public Task<Result<GetUserDto>> ResetPin(ResetPinRequestModel resetPinRequest, string userId);
    public Task<Result<GetUserDto>> CreatepPin(CreatePinRequestModel createPinRequest, string userId);

}
