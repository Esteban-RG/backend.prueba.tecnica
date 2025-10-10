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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthRequests.Login request)
    {
        var result = await _authService.Login(request);
        if (result != null)
        {
            return Ok(result);
        }
        return Unauthorized();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthRequests.Register request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authService.Register(request);

        if (result is IdentityResult identityResult && !identityResult.Succeeded)
        {
            var errorMsg = identityResult.Errors.FirstOrDefault()?.Description ?? "¡La creación del usuario falló! Por favor, revisa los detalles del usuario y vuelve a intentarlo.";
            return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = errorMsg });
        }

        return Ok(result);
    }
}

