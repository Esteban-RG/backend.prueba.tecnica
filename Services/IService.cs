using PruebaBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaBackend.Services
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllPermisosAsync();
        Task<T> GetPermisoByIdAsync(int id);
        Task<T> CreatePermisoAsync(T t);
        Task<bool> UpdatePermisoAsync(T t);
        Task<bool> DeletePermisoAsync(int id);
    }
}
