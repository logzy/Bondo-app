using Bondo.Application.Interfaces;
using Bondo.Application.Models.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bondo.WebAPI;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("Current")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var response = await _userService.GetCurrentUser(User);
        if(response.Succeeded)
            return StatusCode(StatusCodes.Status200OK, response);
        else
            return StatusCode(StatusCodes.Status404NotFound, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _userService.GetUserById(id);
        if(response.Succeeded)
            return StatusCode(StatusCodes.Status200OK, response);
        else
            return StatusCode(StatusCodes.Status404NotFound, response);
    }
    
    [HttpGet("All")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _userService.GetUsers();
        return StatusCode(StatusCodes.Status200OK, response);
    }

    [HttpPost("Pin/Create")]
    public async Task<IActionResult> CreatePin([FromBody] CreatePinRequestModel createPinRequest)
    {
        var user = await _userService.GetCurrentUser(User);
        if(!user.Succeeded)
            return StatusCode(StatusCodes.Status404NotFound, user);
        
        var response = await _userService.CreatepPin(createPinRequest, user.Data.Id);
        if (response.Succeeded)
            return Ok(response);
        else return BadRequest(response);
    }

    [HttpPost("Pin/Reset")]
    public async Task<IActionResult> ResetPin([FromBody] ResetPinRequestModel resetPinRequest)
    {
        var user = await _userService.GetCurrentUser(User);
        if(!user.Succeeded)
            return StatusCode(StatusCodes.Status404NotFound, user);
        
        var response = await _userService.ResetPin(resetPinRequest, user.Data.Id);
        if (response.Succeeded)
            return Ok(response);
        else return BadRequest(response);
    }

    [HttpPost("ForgotPassword")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestModel forgotPasswordRequest)
    {
        // var user = await _userService.GetCurrentUser(User);
        // if(!user.Succeeded)
        //     return StatusCode(StatusCodes.Status404NotFound, user);
        string origin = Request.Headers.ContainsKey("Origin") ? Request.Headers["Origin"].FirstOrDefault() : "https://bondo.com";
        
        var response = await _userService.RequestPasswordReset(forgotPasswordRequest, origin);
        if (response.Succeeded)
            return Ok(response);
        else return BadRequest(response);
    }
    
    [HttpPost("ResetPassword")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword([FromBody] PasswordResetRequestModel passwordResetRequestModel)
    {
        // var user = await _userService.GetCurrentUser(User);
        // if(!user.Succeeded)
        //     return StatusCode(StatusCodes.Status404NotFound, user);
        
        var response = await _userService.ResetPassword(passwordResetRequestModel);
        if (response.Succeeded)
            return Ok(response);
        else return BadRequest(response);
    }

    [HttpPost("Update")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequestModel profileRequestModel)
    {
        var user = await _userService.GetCurrentUser(User);
        if(!user.Succeeded)
            return StatusCode(StatusCodes.Status404NotFound, user);
        
        var response = await _userService.UpdateAccount(profileRequestModel, user.Data.Id);
        if (response.Succeeded)
            return Ok(response);
        else return BadRequest(response);
    }
}
