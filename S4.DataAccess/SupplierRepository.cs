using Microsoft.EntityFrameworkCore;

using S4.DataAccess.Base;
using S4.Entities.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace S4.DataAccess
{
    public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
    {

        public SupplierRepository(DbContext context) : base(context) { }

        public override async Task<Supplier> GetByIdAsync(int? id)
        {
            return await context.Set<Supplier>()
                .Include(s => s.Products)
                .SingleOrDefaultAsync(p => p.SupplierId == id);
        }

        public override async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await context.Set<Supplier>().Include(s => s.Products).ToListAsync();
        }
    }
}