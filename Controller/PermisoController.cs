using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaBackend.DTOs;
using PruebaBackend.Models;
using PruebaBackend.Services;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class PermisoController : ControllerBase
{
    private readonly PermisoService _permisoService;

    public PermisoController(PermisoService permisoService)
    {
        _permisoService = permisoService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<PermisoDTOs.PermisoDTO>>> GetAll()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

        IEnumerable<PermisoDTOs.PermisoDTO> permisosDTOs = Enumerable.Empty<PermisoDTOs.PermisoDTO>();
        if (roles.Contains("Administrador"))
        {
            var permisos = await _permisoService.GetAllAsync();
            permisosDTOs = permisos.Select(PermisoDTOs.FromModel);
        }
        else if (roles.Contains("Empleado"))
        {
            var permisos = await _permisoService.GetMyAsync(userId);
            permisosDTOs = permisos.Select(PermisoDTOs.FromModel);
        }

        return Ok(permisosDTOs);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetById(int id)
    {
        var permiso = await _permisoService.GetByIdAsync(id);
        if (permiso == null) return NotFound();
        var permisoDTO = PermisoDTOs.FromModel(permiso);
        return Ok(permisoDTO);
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Create(PermisoDTOs.NewPermiso newPermiso)
    {
        try
        {
            var permiso = PermisoDTOs.ToModel(newPermiso);
            var nuevoPermiso = await _permisoService.CreateAsync(permiso);
            return CreatedAtAction(nameof(GetById), new { id = nuevoPermiso.Id }, nuevoPermiso);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Update(PermisoDTOs.NewPermiso permisoDTO, int id)
    {
        var permiso = PermisoDTOs.ToModel(permisoDTO);
        permiso.Id = id;
        var result = await _permisoService.UpdateAsync(permiso);
        if (!result) return NotFound();

        return NoContent(); 
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _permisoService.DeleteAsync(id);
        if (!result) return NotFound();

        return NoContent();
    }
}
