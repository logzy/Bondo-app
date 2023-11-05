﻿using Bondo.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bondo.WebAPI;

[Route("api/[controller]")]
[ApiController]
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
        return StatusCode(StatusCodes.Status200OK, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _userService.GetUserById(id);
        return StatusCode(StatusCodes.Status200OK, response);
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _userService.GetUsers();
        return StatusCode(StatusCodes.Status200OK, response);
    }
}