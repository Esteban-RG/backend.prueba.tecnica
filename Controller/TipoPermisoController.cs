using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaBackend.DTOs;
using PruebaBackend.Models;
using PruebaBackend.Services;


[ApiController]
[Authorize(Roles = "Administrador")]
[Route("api/[controller]")]
public class TipoPermisoController : ControllerBase
{
    private readonly IService<TipoPermiso> _tipoPermisoService;

    public TipoPermisoController(IService<TipoPermiso> tipoPermisoService)
    {
        _tipoPermisoService = tipoPermisoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoPermisoDTOs.TipoPermisoDTO>>> GetAll()
    {
        var tipoPermisos = await _tipoPermisoService.GetAllPermisosAsync();
        var tipoPermisoDTOs = tipoPermisos.Select(TipoPermisoDTOs.FromModel);
        return Ok(tipoPermisoDTOs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var permiso = await _tipoPermisoService.GetPermisoByIdAsync(id);
        if (permiso == null) return NotFound();
        var permisoDTO = TipoPermisoDTOs.FromModel(permiso);
        return Ok(permisoDTO);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TipoPermisoDTOs.NewTipoPermiso newTipoPermiso)
    {
        try
        {
            var tipoPermiso = TipoPermisoDTOs.ToModel(newTipoPermiso);
            var nuevoTipoPermiso = await _tipoPermisoService.CreatePermisoAsync(tipoPermiso);
            return CreatedAtAction(nameof(GetById), new { id = nuevoTipoPermiso.Id }, nuevoTipoPermiso);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(TipoPermisoDTOs.TipoPermisoDTO tipoPermisoDTO, int id)
    {
        if (id != tipoPermisoDTO.Id) return BadRequest();
        var tipoPermiso = TipoPermisoDTOs.ToModel(tipoPermisoDTO);
        var result = await _tipoPermisoService.UpdatePermisoAsync(tipoPermiso);
        if (!result) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _tipoPermisoService.DeletePermisoAsync(id);
        if (!result) return NotFound();

        return NoContent();
    }

}
