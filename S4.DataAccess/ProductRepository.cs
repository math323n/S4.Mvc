using Microsoft.EntityFrameworkCore;
using S4.DataAccess.Base;
using S4.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace S4.DataAccess
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {

        }

        public override async Task<Product> GetByIdAsync(int? id)
        {
            return await context.Set<Product>()
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await context.Set<Product>()
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .ToListAsync();
        }
    }
}