using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PruebaBackend.DTOs;
using PruebaBackend.Models;
using PruebaBackend.Services;
using System.Security.Claims;
using static PruebaBackend.DTOs.PermisoDTOs;

[ApiController]
[Route("api/[controller]")]
public class PermisoController : ControllerBase
{
    private readonly PermisoService _permisoService;
    private readonly UserManager<Usuario> _userManager;

    public PermisoController(PermisoService permisoService, UserManager<Usuario> userManager)
    {
        _permisoService = permisoService;
        _userManager = userManager;

    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<PermisoDTOs.PermisoDTO>>> GetAll()
    {
       
        var permisos = await _permisoService.GetAllAsync();
        var permisosDTOs = permisos.Select(PermisoDTOs.FromModel);

        return Ok(permisosDTOs);
    }

    [HttpGet("MisPermisos")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<PermisoDTOs.PermisoDTO>>> GetMy()
    {

        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var permisos = await _permisoService.GetMyAsync(userId);
        var permisosDTOs = permisos.Select(PermisoDTOs.FromModel);

        return Ok(permisosDTOs);
    }

    [HttpGet("EstatusPendiente")]
    [Authorize(Roles = "Administrador, Supervisor")]
    public async Task<ActionResult<IEnumerable<PermisoDTOs.PermisoDTO>>> GetPendientes()
    {
        var permisos = await _permisoService.GetPendientesAsync();
        var permisosDTOs = permisos.Select(PermisoDTOs.FromModel);
        return Ok(permisosDTOs);
    }

    [HttpGet("Search")]
    public async Task<ActionResult<IEnumerable<PermisoDTOs.PermisoDTO>>> SearchPermisos(
        [FromQuery] int? TipoPermisoId,      
        [FromQuery] int? IdEstatusPermiso     
        )
    {
        var permisos = await _permisoService.SearchPermisosAsync(TipoPermisoId, IdEstatusPermiso);
        var permisosDTOs = permisos.Select(PermisoDTOs.FromModel);
        return Ok(permisosDTOs);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Administrador, Supervisor")]
    public async Task<IActionResult> GetById(int id)
    {
        var permiso = await _permisoService.GetByIdAsync(id);
        if (permiso == null) return NotFound();
        var permisoDTO = PermisoDTOs.FromModel(permiso);
        return Ok(permisoDTO);
    }

    [HttpPost("Solicitud")]
    [Authorize(Roles = "Empleado")]
    public async Task<IActionResult> CreateSolicitud(PermisoDTOs.NewSolicitud newSolicitud)
    {
        try
        {
            var idUsuarioString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(idUsuarioString)) return Unauthorized();

            var usuario = await _userManager.FindByIdAsync(idUsuarioString);
            if (usuario == null) return NotFound("Usuario no encontrado.");

            var newPermiso = PermisoDTOs.ToNewPermiso(newSolicitud, usuario);

            var nuevoPermiso = await _permisoService.CreateAsync(newPermiso);
            var permisoDTO = PermisoDTOs.FromModel(nuevoPermiso);

            return CreatedAtAction(nameof(GetById), new { id = permisoDTO.Id }, permisoDTO);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> CreateByAdmin(PermisoDTOs.NewPermiso newPermiso)
    {
        try
        {
            var nuevoPermiso = await _permisoService.CreateAsync(newPermiso);

            var permisoDTO = PermisoDTOs.FromModel(nuevoPermiso);
            return CreatedAtAction(nameof(GetById), new { id = permisoDTO.Id }, permisoDTO);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPatch("Aprobar/{id}")]
    [Authorize(Roles = "Administrador, Supervisor")]
    public async Task<IActionResult> Approve(int id)
    {
        var result = await _permisoService.ApproveAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpPatch("Rechazar/{id}")]
    [Authorize(Roles = "Administrador, Supervisor")]
    public async Task<IActionResult> Deny(int id, [FromBody] PermisoDTOs.ApproveStatusDTO dto)
    {
        var result = await _permisoService.DenyAsync(id, dto.ComentariosSupervisor);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Update(PermisoDTOs.PermisoDTO dto)
    {
        var result = await _permisoService.UpdateAsync(dto);
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
