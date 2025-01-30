using AppDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly ApiDbContext _context;
    
    public UsersController(ILogger<UsersController> logger, ApiDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetAll")]
    public async Task<IActionResult> Get()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(users);
    }
}