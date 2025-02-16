using ExpenseTrackerApi.Data;
using ExpenseTrackerApi.Models.Dtos;
using ExpenseTrackerApi.Models.Interfaces;
using ExpenseTrackerApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpenseTrackerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserRepository _userService;

    public UsersController(ILogger<UsersController> logger, ApiDbContext context, IUserRepository userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [Authorize]
    [HttpPost("Create")]
    public async Task<IActionResult> Create(UserCreateRequest user)
    {
        try
        {
            await _userService.Create(user);
            return Ok("User created successfully.");
        }
        catch (Exception ex) 
        { 
            return BadRequest("Error creating user. Error: " + ex.Message);
        }
    }

    [Authorize]
    [HttpGet(Name = "GetAll")]
    public async Task<IActionResult> Get()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }
}