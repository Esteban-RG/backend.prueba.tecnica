using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PruebaBackend.DTOs;
using PruebaBackend.Services;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] AuthDTO.Login request)
    {
        var result = await _authService.Login(request);
        if (result.Succeeded)
        {
            return Ok(new { token = result.Token});
        }
        return BadRequest(new { message = result.Errors });
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] AuthDTO.Register request)
    {

        var result = await _authService.Register(request);

        if (result.Succeeded)
        {
            return Ok(new { token = result.Token });
        }

        return BadRequest(new { message = result.Errors });
    }
}

