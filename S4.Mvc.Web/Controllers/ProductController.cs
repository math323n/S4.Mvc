using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using S4.DataAccess;
using S4.DataAccess.Base;
using S4.Entities.Models;

namespace S4.Mvc.Web.Controllers
{
    public class ProductController: Controller
    {
        private readonly IProductRepository productRepo;
        private readonly IRepositoryBase<Category> categoryRepo;
        private readonly ISupplierRepository supplierRepo;
        public ProductController(
            IProductRepository productRepo,
            IRepositoryBase<Category> categoryRepo,
            ISupplierRepository supplierRepo)
        {
            this.productRepo = productRepo;
            this.supplierRepo = supplierRepo;
            this.categoryRepo = categoryRepo;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await productRepo.GetAllAsync();

            return View(products);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Product product = await productRepo.GetByIdAsync(id);

            if(product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            IEnumerable<Category> categories = await categoryRepo.GetAllAsync();
            IEnumerable<Supplier> suppliers = await supplierRepo.GetAllAsync();

            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(suppliers, "SupplierId", "CompanyName");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Product product)
        {
            if(ModelState.IsValid)
            {
                await productRepo.AddAsync(product);

                return RedirectToAction(nameof(Index));
            }

            IEnumerable<Category> categories = await categoryRepo.GetAllAsync();
            IEnumerable<Supplier> suppliers = await supplierRepo.GetAllAsync();

            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(suppliers, "SupplierId", "CompanyName", product.SupplierId);

            return View(product);
        }

        //// GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Product product = await productRepo.GetByIdAsync(id);

            if(product == null)
            {
                return NotFound();
            }

            IEnumerable<Category> categories = await categoryRepo.GetAllAsync();
            IEnumerable<Supplier> suppliers = await supplierRepo.GetAllAsync();

            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(suppliers, "SupplierId", "CompanyName", product.SupplierId);

            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Product product)
        {
            if(id != product.ProductId)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    await productRepo.UpdateAsync(product);
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!await ProductExistsAsync(product.ProductId))
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

            IEnumerable<Category> categories = await categoryRepo.GetAllAsync();
            IEnumerable<Supplier> suppliers = await supplierRepo.GetAllAsync();

            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(suppliers, "SupplierId", "CompanyName", product.SupplierId);

            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Product product = await productRepo.GetByIdAsync(id);

            if(product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Product product = await productRepo.GetByIdAsync(id);

            await productRepo.DeleteAsync(product);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExistsAsync(int id)
        {
            Product result = await productRepo.GetByIdAsync(id);

            return (result != null);
        }
    }
}