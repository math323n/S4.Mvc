
using S4.Entities.Models.Context;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace S4.DataAccess.Base
{
    /// <summary>
    /// Base repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryBase<T>
    {
        Task AddAsync(T t);
        Task<T> GetByIdAsync(int? id);
        Task<IEnumerable<T>> GetAllAsync();
        Task UpdateAsync(T t);
        Task DeleteAsync(T t);
    }
}
