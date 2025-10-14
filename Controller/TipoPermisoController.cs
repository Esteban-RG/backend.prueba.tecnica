using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaBackend.DTOs;
using PruebaBackend.Models;
using PruebaBackend.Services;


[ApiController]
[Route("api/[controller]")]
public class TipoPermisoController : ControllerBase
{
    private readonly TipoPermisoService _tipoPermisoService;

    public TipoPermisoController(TipoPermisoService tipoPermisoService)
    {
        _tipoPermisoService = tipoPermisoService;
    }

    [HttpGet]
    [Authorize]

    public async Task<ActionResult<IEnumerable<TipoPermisoDTOs.TipoPermisoDTO>>> GetAll()
    {
        var tipoPermisos = await _tipoPermisoService.GetAllAsync();
        var tipoPermisoDTOs = tipoPermisos.Select(TipoPermisoDTOs.FromModel);
        return Ok(tipoPermisoDTOs);
    }

    [HttpGet("{id}")]
    [Authorize]

    public async Task<IActionResult> GetById(int id)
    {
        var permiso = await _tipoPermisoService.GetByIdAsync(id);
        if (permiso == null) return NotFound();
        var permisoDTO = TipoPermisoDTOs.FromModel(permiso);
        return Ok(permisoDTO);
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]

    public async Task<IActionResult> Create(TipoPermisoDTOs.NewTipoPermiso newTipoPermiso)
    {
        try
        {
            var tipoPermiso = TipoPermisoDTOs.ToModel(newTipoPermiso);
            var nuevoTipoPermiso = await _tipoPermisoService.CreateAsync(tipoPermiso);
            return CreatedAtAction(nameof(GetById), new { id = nuevoTipoPermiso.Id }, nuevoTipoPermiso);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Administrador")]

    public async Task<IActionResult> Update(TipoPermisoDTOs.TipoPermisoDTO tipoPermisoDTO, int id)
    {
        if (id != tipoPermisoDTO.Id) return BadRequest();
        var tipoPermiso = TipoPermisoDTOs.ToModel(tipoPermisoDTO);
        var result = await _tipoPermisoService.UpdateAsync(tipoPermiso);
        if (!result) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]

    public async Task<IActionResult> Delete(int id)
    {
        var result = await _tipoPermisoService.DeleteAsync(id);
        if (!result) return NotFound();

        return NoContent();
    }

}
