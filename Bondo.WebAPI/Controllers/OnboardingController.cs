using Microsoft.AspNetCore.Mvc;
using Bondo.Application.Interfaces;
using Bondo.Application.DTOs.Otp;
using Bondo.Application.Models.User;

namespace Bondo.WebAPI;

[Route("api/[controller]")]
[ApiController]
public class OnboardingController : ControllerBase
{
    private readonly IOtpService _otpService;
    private readonly IUserService _userService;

    public OnboardingController(IOtpService otpService, IUserService userService)
    {
        _otpService = otpService;
        _userService = userService;
    }
    [HttpPost("PhoneOtp/Request")]
    public async Task<IActionResult> RequestPhoneOtp([FromBody] SendOtpRequestDto phoneOtpRequest)
    {
        var response = await _otpService.CreatePhoneOtp(phoneOtpRequest);
        if (response.Succeeded)
            return Ok(response);
        else return BadRequest(response);
    }
    
    [HttpPost("EmailOtp/Request")]
    public async Task<IActionResult> RequestEmailOtp([FromBody] SendOtpRequestDto emailOtpRequest)
    {
        var response = await _otpService.CreateEmailOtp(emailOtpRequest);
        if (response.Succeeded)
            return Ok(response);
        else return BadRequest(response);
    }

    [HttpPost("VerifyOtp")]
    public async Task<IActionResult> VerifyOtp([FromBody] ConfirmOtpRequestDto confirmOtpRequest)
    {
        var response = await _otpService.VerifyOtp(confirmOtpRequest);
        if (response.Succeeded)
            return Ok(response);
        else return BadRequest(response);
    }


    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestModel accountRequest)
    {
        var response = await _userService.RegisterAccount(accountRequest);
        if (response.Succeeded)
            return Ok(response);
        else return BadRequest(response);
    }
    
}

