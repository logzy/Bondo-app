using Bondo.Application;
using Bondo.Application.Interfaces;
using Bondo.Application.Models.Contract;
using Bondo.Domain.Enums;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bondo.WebAPI;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
public class ContractController : ControllerBase
{
    private readonly IContractService _contractService;
    private readonly IUserService _userService;

    public ContractController(IContractService contractService,
    IUserService userService)
    {
        _contractService = contractService;
        _userService = userService;
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAllContracts()
    {
        var response = await _contractService.GetContracts();
        if(response.Succeeded)
            return StatusCode(StatusCodes.Status200OK, response);
        else
            return StatusCode(StatusCodes.Status404NotFound, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContractById(int id)
    {
        var response = await _contractService.GetContractById(id);
        if(response.Succeeded)
            return StatusCode(StatusCodes.Status200OK, response);
        else
            return StatusCode(StatusCodes.Status404NotFound, response);
    }

    [HttpGet("owner/{ownerId}")]
    public async Task<IActionResult> GetContractByOwner(string ownerId)
    {
        var response = await _contractService.GetContractsByOwner(ownerId);
        if(response.Succeeded)
            return StatusCode(StatusCodes.Status200OK, response);
        else
            return StatusCode(StatusCodes.Status404NotFound, response);
    }

    [HttpGet("contractor/{contractorId}")]
    public async Task<IActionResult> GetContractByContractor(string contractorId)
    {
        var response = await _contractService.GetContractsByContractor(contractorId);
        if(response.Succeeded)
            return StatusCode(StatusCodes.Status200OK, response);
        else
            return StatusCode(StatusCodes.Status404NotFound, response);
    }
    
    [HttpPost("Create")]
    public async Task<IActionResult> CreateContract([FromBody] CreateContractRequestModel requestModel)
    {
        var user = await _userService.GetCurrentUser(User);
        var response = await _contractService.CreateContract(requestModel, user.Data.Id);
        if(response.Succeeded)
            return StatusCode(StatusCodes.Status200OK, response);
        else
            return StatusCode(StatusCodes.Status404NotFound, response);
    }
    
    [HttpPost("Update")]
    public async Task<IActionResult> UpdateContract([FromBody] UpdateContractRequestModel requestModel)
    {
        var user = await _userService.GetCurrentUser(User);
        var response = await _contractService.UpdateContract(requestModel, user.Data.Id);
        if(response.Succeeded)
            return StatusCode(StatusCodes.Status200OK, response);
        else
            return StatusCode(StatusCodes.Status404NotFound, response);
    }

    [HttpPost("Apply")]
    public async Task<IActionResult> ApplyToContract([FromBody] ContractApplicationRequestModel requestModel)
    {
        var user = await _userService.GetCurrentUser(User);
        var response = await _contractService.ApplyToContract(requestModel, user.Data.Id);
        if(response.Succeeded)
            return StatusCode(StatusCodes.Status200OK, response);
        else
            return StatusCode(StatusCodes.Status404NotFound, response);
    }
    
    [HttpPost("{contractId}/Status/{status}")]
    public async Task<IActionResult> ChangeStatus(int contractId, int status)
    {
        // var user = await _userService.GetCurrentUser(User);
        var response = await _contractService.ChangeStatus(contractId, (ContractEnums.ContractStatus)status);
        if(response.Succeeded)
            return StatusCode(StatusCodes.Status200OK, response);
        else
            return StatusCode(StatusCodes.Status404NotFound, response);
    }
}
