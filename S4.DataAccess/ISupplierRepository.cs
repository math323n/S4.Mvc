using S4.Entities.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace S4.DataAccess
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAllAsync();
        Task<Supplier> GetByIdAsync(int? id);
    }
}