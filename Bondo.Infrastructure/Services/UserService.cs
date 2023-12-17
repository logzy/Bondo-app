using System.Security.Claims;
using AutoMapper;
using Bondo.Application;
using Bondo.Application.Interfaces;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Application.Models.User;
using Bondo.Domain.Entities;
using Bondo.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Bondo.Infrastructure.Services;
public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepository;
    public UserService(UserManager<ApplicationUser> userManager,
        IMapper mapper, SignInManager<ApplicationUser> signInManager,
        IConfiguration config, IUserRepository userRepository)
    {
        // _validateUserInfo = new ValidateUserInfo();
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
        _config = config;
        _userRepository = userRepository;
    }
    public async Task<Result<GetUserDto>> GetCurrentUser(ClaimsPrincipal principal)
    {
        foreach (var claim in principal.Claims)
        {
           Console.WriteLine(claim.Value); 
        }
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
        var userDto = _mapper.Map<GetUserDto>(user);
        return Result<GetUserDto>.Success(userDto);

    }

    // Helper
    private async Task<bool> EmailExistsAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return false;
        else return true;
    }

}
