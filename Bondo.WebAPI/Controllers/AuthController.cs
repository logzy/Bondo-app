using System.Security.Claims;
using Bondo.Application;
using Bondo.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bondo.WebAPI;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly IUserService _userService;
    public AuthController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel loginRequest)
    {
        var response = await _userService.Login(loginRequest);
        if (response.Succeeded){
          var claims = new Claim[]
          {
            new Claim(ClaimTypes.NameIdentifier, response.Data.Id),
            // new Claim("Name", response.Data.UserName)
          };

          var claimsIdentity = new ClaimsIdentity(
          claims, CookieAuthenticationDefaults.AuthenticationScheme);

          HttpContext.SignInAsync(
          CookieAuthenticationDefaults.AuthenticationScheme,
          new ClaimsPrincipal(claimsIdentity)).Wait();
          return Ok(response);

        }
        else return BadRequest(response);
    }

    [HttpGet]
    [Route("logout")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public IActionResult Logout()
    {
      try
      {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
        return Ok();
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }
}
