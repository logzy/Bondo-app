using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Bondo.Application;
using Bondo.Application.DTOs.Email;
using Bondo.Application.Interfaces;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Application.Models.User;
using Bondo.Domain.Entities;
using Bondo.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;

namespace Bondo.Infrastructure.Services;
public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    public UserService(UserManager<ApplicationUser> userManager,
        IMapper mapper, SignInManager<ApplicationUser> signInManager,
        IConfiguration config, IUserRepository userRepository,
        IEmailService emailService)
    {
        // _validateUserInfo = new ValidateUserInfo();
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
        _config = config;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<Result<GetUserDto>> CreatepPin(CreatePinRequestModel createPinRequest, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if(user == null)
            return Result<GetUserDto>.Failure($"user with id {userId} not found");
        string hashedPin = GetHashString(createPinRequest.Pin);
        user.PinHash = hashedPin;

        await _userManager.UpdateAsync(user);
        return Result<GetUserDto>.Success(_mapper.Map<GetUserDto>(user), "User pin created");
    }

    public async Task<Result<GetUserDto>> GetCurrentUser(ClaimsPrincipal principal)
    {
        // foreach (var claim in principal.Claims)
        // {
        //    Console.WriteLine(claim.Value); 
        // }
        var user = await _userManager.GetUserAsync(principal);
        if(user != null){
            var userDto = _mapper.Map<GetUserDto>(user);
            return Result<GetUserDto>.Success(userDto);
        }
        return Result<GetUserDto>.Failure("Failed to get current user");
    }

    public async Task<Result<GetUserDto>> GetUserById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if(user != null){
            var userDto = _mapper.Map<GetUserDto>(user);
            return Result<GetUserDto>.Success(userDto);
        }
        return Result<GetUserDto>.Failure("User not found");
    }

    public async Task<Result<List<GetUserDto>>> GetUsers()
    {
        var users = await _userRepository.GetAll();
        List<GetUserDto> userDto = _mapper.Map<List<GetUserDto>>(users);
        return Result<List<GetUserDto>>.Success(userDto);
    }

    public async Task<Result<GetUserDto>> Login(LoginRequestModel loginRequest)
    {
        var user = await _userManager.FindByNameAsync(loginRequest.UserName);
        if(user == null)
            return Result<GetUserDto>.Failure("Username of password invalid");
        // var signinResult = await _signInManager.PasswordSignInAsync(user, loginRequest.Password,loginRequest.RememberMe, false);
        var passwordCheck = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
        if(passwordCheck){
            GetUserDto userDto = _mapper.Map<GetUserDto>(user);
            return Result<GetUserDto>.Success(userDto);
        }

        // something went wrong
        return Result<GetUserDto>.Failure("Username of password invalid");

    }

    public async Task<Result<GetUserDto>> RegisterAccount(RegisterRequestModel registerAccount)
    {
        var validation = registerAccount.Validate();
        if(!validation.Success){
            return Result<GetUserDto>.Failure(validation.Errors);
        }
        ApplicationUser user = new ApplicationUser{
            FirstName = registerAccount.FirstName,
            LastName = registerAccount.LastName,
            Email = registerAccount.Email,
            PhoneNumber = registerAccount.PhoneNumber,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            UserName = registerAccount.Email
        };
        // user.EmailConfirmed = true;
        // user.PhoneNumberConfirmed = true;
        // user.UserName = registerAccount.Email;
        if(await EmailExistsAsync(registerAccount.Email))
            return Result<GetUserDto>.Failure("Email already exists");
        var result = await _userManager.CreateAsync(user, registerAccount.Password);
        if(!result.Succeeded)
            return Result<GetUserDto>.Failure("An error occured while creating your account");

        // sign in user
        var res = await Login(new LoginRequestModel { Password = registerAccount.Password, UserName = registerAccount.Email });
        
        // send email
        EmailRequestDto emailRequest = new EmailRequestDto{
            to = new List<EmailRequestDto.Recepient>{new EmailRequestDto.Recepient{
                name = user.FirstName,
                email = user.Email
            }},
            subject = "WELCOME",
            body = $"<p>Dear {user.FirstName},</p><p>Your Bondo account has been created successfully.</p>"
        };
        await _emailService.SendAsync(emailRequest);
        var userDto = _mapper.Map<GetUserDto>(user);
        return Result<GetUserDto>.Success(userDto);

    }

    public async Task<Result<bool>> RequestPasswordReset(ForgotPasswordRequestModel requestModel, string origin)
    {
        var user = await _userManager.FindByEmailAsync(requestModel.Email);
        if(user == null)
            return Result<bool>.Failure(false, "user not found");

        string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        resetToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(resetToken));
        origin = string.IsNullOrEmpty(origin) ? _config["BaseClientUrl"] : origin;
        string callback = $"{origin}/{_config["BaseClientPasswordResetRoute"]}?id={user.Id}&code={resetToken}";
        
        EmailRequestDto emailRequest = new EmailRequestDto{
            to = new List<EmailRequestDto.Recepient>{new EmailRequestDto.Recepient{
                name = user.FirstName,
                email = user.Email
            }},
            subject = "PASSWORD RESET REQUEST",
            body = $"Click <a href={callback}>HERE</a> to reset your password."
        };

        var result = await _emailService.SendAsync(emailRequest);

        if(result.Succeeded)
            return Result<bool>.Success(true, "Password reset email sent!");
        
        // something went wrong
        return Result<bool>.Success(false, "Password reset not sent!");
        
    }

    public async Task<Result<GetUserDto>> ResetPassword(PasswordResetRequestModel passwordResetRequest)
    {
        var user = await _userManager.FindByIdAsync(passwordResetRequest.Id);
        if(user == null)
            return Result<GetUserDto>.Failure( "user not found");
        string token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(passwordResetRequest.Token));

        var result = await _userManager.ResetPasswordAsync(user, token, passwordResetRequest.Password);
        if(result.Succeeded)
            return Result<GetUserDto>.Success("Password reset successfully");

        return Result<GetUserDto>.Failure("Error resetting user's password");
    }

    public async Task<Result<GetUserDto>> ResetPin(ResetPinRequestModel resetPinRequest, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if(user == null)
            return Result<GetUserDto>.Failure( "user not found");
        string oldPinHash = GetHashString(resetPinRequest.OldPin);

        if(oldPinHash != user.PinHash)
            return Result<GetUserDto>.Failure("Invalid old pin");
        user.PinHash = GetHashString(resetPinRequest.NewPin);

        await _userManager.UpdateAsync(user);
        return Result<GetUserDto>.Success("Pin reset successfully");
    }

    public async Task<Result<GetUserDto>> UpdateAccount(UpdateProfileRequestModel updateProfileRequest, string userId)
    {
        var validation = updateProfileRequest.Validate();
        if(!validation.Success)
            return Result<GetUserDto>.Failure(validation.Errors);
        var user = await _userManager.FindByIdAsync(userId);
        if(user == null)
            return Result<GetUserDto>.Failure( "user not found");


        user.FirstName = updateProfileRequest.FirstName;
        user.LastName = updateProfileRequest.LastName;
        user.PhoneNumber = updateProfileRequest.PhoneNumber;

        var updateResult = await _userManager.UpdateAsync(user);
        if(updateResult.Succeeded)
            return Result<GetUserDto>.Success(_mapper.Map<GetUserDto>(user), "User profile updated successfully");
        
        return Result<GetUserDto>.Failure("An error occoured while updating user");
    }


    // Helper
    private async Task<bool> EmailExistsAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return false;
        else return true;
    }
    private byte[] GetHash(string inputString)
    {
        using (HashAlgorithm algorithm = SHA256.Create())
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }

    private string GetHashString(string inputString)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in GetHash(inputString))
            sb.Append(b.ToString("X2"));

        return sb.ToString();
    }

}
