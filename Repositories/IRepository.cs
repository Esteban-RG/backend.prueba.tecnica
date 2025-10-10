using PruebaBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaBackend.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T t);
        void Update(T t);
        void Delete(T t);
        Task<bool> SaveChangesAsync(); 
    }
}
