using Microsoft.AspNetCore.Identity;
using PruebaBackend.DTOs;
using PruebaBackend.Models;
using PruebaBackend.Repositories;
using System.Threading.Tasks;

namespace PruebaBackend.Services
{
    public class AuthService
    {
        private readonly JwtService _jwtService;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public AuthService(JwtService jwtService, UserManager<Usuario> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _jwtService = jwtService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<AuthResult> Login(AuthDTO.Login request)
        {
            var user = await _userManager.FindByEmailAsync(request.email);
            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var token = _jwtService.GenerateToken(user, userRoles);

                return AuthResult.Success(token);
            }
            return AuthResult.Failure(new[] { "Credenciales inválidas" });
        }

        public async Task<AuthResult> Register(AuthDTO.Register request)
        {
            var userExists = await _userManager.FindByNameAsync(request.Username);
            if (userExists != null)
            {
                return AuthResult.Failure( new[] {"El usuario ya existe."} );
            }

            Usuario user = new Usuario()
            {
                Nombre = request.Nombre,
                Apellidos = request.Apellidos,
                Email = request.Email,
                SecurityStamp = System.Guid.NewGuid().ToString(),
                UserName = request.Username
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return AuthResult.Failure(errors);
            }

            
            await _roleManager.CreateAsync(new IdentityRole<int>("Empleado"));
         

            await _userManager.AddToRoleAsync(user, "Empleado");

            var userRoles = await _userManager.GetRolesAsync(user);
            var token = _jwtService.GenerateToken(user, userRoles);

            return AuthResult.Success(token);
        }
    }
}
