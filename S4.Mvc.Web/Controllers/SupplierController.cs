using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S4.DataAccess.Base;
using S4.Entities.Models;

namespace S4.Mvc.Web.Controllers
{
    public class SupplierController : Controller
    {
        private readonly IRepositoryBase<Supplier> repository;

        public SupplierController(IRepositoryBase<Supplier> supplierRepository)
        {
            repository = supplierRepository;
        }


        // GET: Supplier
        public async Task<IActionResult> Index()
        {
            IEnumerable<Supplier> suppliers = await repository.GetAllAsync();

            return View(suppliers);
        }

        // GET: Supplier/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Supplier supplier = await repository.GetByIdAsync(id);

            if(supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Supplier/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierId,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax,HomePage")] Supplier supplier)
        {
            if(ModelState.IsValid)
            {
                await repository.AddAsync(supplier);

                return RedirectToAction(nameof(Index));
            }

            return View(supplier);
        }

        // GET: Supplier/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Supplier supplier = await repository.GetByIdAsync(id);

            if(supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierId,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax,HomePage")] Supplier supplier)
        {
            if(id != supplier.SupplierId)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    await repository.UpdateAsync(supplier);
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!await SupplierExistsAsync(supplier.SupplierId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(supplier);
        }

        // GET: Supplier/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Supplier supplier = await repository.GetByIdAsync(id);

            if(supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Supplier supplier = await repository.GetByIdAsync(id);

            await repository.DeleteAsync(supplier);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> SupplierExistsAsync(int id)
        {
            Supplier supplier = await repository.GetByIdAsync(id);

            return supplier != null;
        }
    }
}