using Microsoft.EntityFrameworkCore;
using S4.Entities.Models.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace S4.DataAccess.Base
{
    /// <summary>
    /// Base generic repository class for encapsulation of DBContext functionalities
    /// </summary>
    /// <typeparam name = "T" ></ typeparam >
    public class RepositoryBase<T>: IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Database context
        /// </summary>
        protected NorthwindContext context;

        /// <summary>
        /// Sets the context to the provided parameter item
        /// </summary>
        /// <param name="context"></param>
        public RepositoryBase(NorthwindContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Initializes the context
        /// </summary>
        public RepositoryBase()
        {
            context = new NorthwindContext();
        }

        /// <summary>
        /// The database context
        /// </summary>
        public virtual NorthwindContext Context
        {
            get { return context; }
            set { context = value; }
        }

        /// <summary>
        /// Adds an item
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual async Task AddAsync(T t)
        {
            context.Set<T>().Add(t);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets an item by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetByIdAsync(int? id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Gets all the items
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Updates an item
        /// </summary>
        /// <param name="t"></param>
        public virtual async Task UpdateAsync(T t)
        {
            context.Set<T>().Update(t);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an item
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(T t)
        {
            context.Set<T>().Remove(t);
            await context.SaveChangesAsync();
        }
    }
}
