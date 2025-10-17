
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PruebaBackend.DTOs;
using PruebaBackend.Models;

namespace PruebaBackend.Services
{
    public class UserService
    {
        private readonly UserManager<Usuario> _userManager;

        public UserService(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<UsuarioDTOs.UsuarioInfo>> GetAllUsersAsync()
        {
            var usuarios = await _userManager.Users.ToListAsync();
            var usuariosDTOs = usuarios.Select(UsuarioDTOs.FromModel);
            return usuariosDTOs;
        }

        internal async Task<UsuarioDTOs.UsuarioInfo> GetUserByIdAsync(int id)
        {
            var usuario = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null) throw new KeyNotFoundException("Usuario no encontrado");
            return UsuarioDTOs.FromModel(usuario);
        }
    }
}
