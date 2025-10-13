
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet("Session")]
    [Authorize]
    public IActionResult GetMisPermisos()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var username = User.FindFirstValue(ClaimTypes.Name);
        var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value)
                         .ToList();

        return Ok(new
        {
            id = userId,
            username = username,
            roles = roles
        });
    }

    [HttpGet("isAdmin")]
    [Authorize(Roles = "Administrador")]
    public IActionResult isAdmin()
    {
        return Ok(new
        {
            message = "true"
        });
    }

    [HttpGet("isEmp")]
    [Authorize(Roles = "Empleado")]
    public IActionResult isEmployee()
    {
        return Ok(new
        {
            message = "true"
        });
    }

}

