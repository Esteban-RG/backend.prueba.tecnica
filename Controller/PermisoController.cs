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
    private readonly IService<Permiso> _permisoService;

    public PermisoController(IService<Permiso> permisoService)
    {
        _permisoService = permisoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PermisoDTOs.PermisoDTO>>> GetAll()
    {
        var permisos = await _permisoService.GetAllPermisosAsync();
        var permisosDTOs = permisos.Select(PermisoDTOs.FromModel);
        return Ok(permisosDTOs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var permiso = await _permisoService.GetPermisoByIdAsync(id);
        if (permiso == null) return NotFound();
        var permisoDTO = PermisoDTOs.FromModel(permiso);
        return Ok(permisoDTO);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PermisoDTOs.NewPermiso newPermiso)
    {
        try
        {
            var permiso = PermisoDTOs.ToModel(newPermiso);
            var nuevoPermiso = await _permisoService.CreatePermisoAsync(permiso);
            return CreatedAtAction(nameof(GetById), new { id = nuevoPermiso.Id }, nuevoPermiso);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(PermisoDTOs.NewPermiso permisoDTO, int id)
    {
        var permiso = PermisoDTOs.ToModel(permisoDTO);
        permiso.Id = id;
        var result = await _permisoService.UpdatePermisoAsync(permiso);
        if (!result) return NotFound();

        return NoContent(); 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _permisoService.DeletePermisoAsync(id);
        if (!result) return NotFound();

        return NoContent();
    }


    [HttpGet("MisPermisos")]
    [Authorize]
    public void GetMisPermisos()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        
    }
}
