using S4.Entities.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace S4.DataAccess
{
    public interface ISupplierRepository
    {
        Task AddAsync(Supplier t);
        Task<Supplier> GetByIdAsync(int? id);
        Task<IEnumerable<Supplier>> GetAllAsync();
        Task UpdateAsync(Supplier t);
        Task DeleteAsync(Supplier t);
    }
}