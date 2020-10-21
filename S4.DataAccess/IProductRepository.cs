using S4.Entities.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace S4.DataAccess
{
    public interface IProductRepository
    {
        Task AddAsync(Product t);
        Task<Product> GetByIdAsync(int? id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task UpdateAsync(Product t);
        Task DeleteAsync(Product t);
    }
}