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

        public async Task<object> Login(AuthRequests.Login request)
        {
            var user = await _userManager.FindByEmailAsync(request.email);
            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                return _jwtService.GenerateToken(user, userRoles);
            }
            return null;
        }

        public async Task<object> Register(AuthRequests.Register request)
        {
            var userExists = await _userManager.FindByNameAsync(request.Username);
            if (userExists != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "El usuario ya existe!" });
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
                return result;
            }

            
            await _roleManager.CreateAsync(new IdentityRole<int>("Empleado"));
         

            await _userManager.AddToRoleAsync(user, "Empleado");

            var userRoles = await _userManager.GetRolesAsync(user);
            return _jwtService.GenerateToken(user, userRoles);
        }
    }
}
