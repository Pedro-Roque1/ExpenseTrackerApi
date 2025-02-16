using ExpenseTrackerApi.Data;
using ExpenseTrackerApi.Models.Dtos;
using ExpenseTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly ApiDbContext _context;
        private readonly AuthService _authService;

        public AuthController(ILogger<UsersController> logger, ApiDbContext context, AuthService authService)
        {
            _logger = logger;
            _context = context;
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login(UserLoginRequest loginRequest)
        {
            string token = await _authService.LoginUser(loginRequest);
            Response.Headers.Add("Authorization", "Bearer " + token);

            return Ok(token);
        }
    }
}
