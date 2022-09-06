using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManageShop.Data;
using ManageShop.Models;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ManageShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ShopContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductsController(ShopContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                Response.Redirect("Login/Index");
            }
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            var shopContext = _context.Products
                .Include(p => p.Category)
                .AsNoTracking();
            return View(await shopContext.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        private void PopulateDepartmentsDropDownList(object selectedcate = null)
        {
            var departmentsQuery = from d in _context.Categories
                                   orderby d.Name // Sort by name. 
                                   select d;
            ViewBag.CategoryID = new SelectList(departmentsQuery
                                   .AsNoTracking(),
                                   "CategoryID",
                                   "Name",
                                   selectedcate);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            
            PopulateDepartmentsDropDownList();
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameProduct,Price,Description,ImageFile,CategoryID")] Product product)
        {
            if (ModelState.IsValid)
            {
                //lưu ảnh vào wwwRoot
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                string extension = Path.GetExtension(product.ImageFile.FileName);
                product.Image = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", filename);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(fileStream);
                }

                _context.Add(product);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            PopulateDepartmentsDropDownList(product.CategoryID);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            PopulateDepartmentsDropDownList(product.CategoryID);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameProduct,Price,Description,ImageFile,CategoryID")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            /*string wwwRootPath = _hostEnvironment.WebRootPath;
            string filename = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
            string extension = Path.GetExtension(product.ImageFile.FileName);
            product.Image = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Image/", filename);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await product.ImageFile.CopyToAsync(fileStream);
            }*/
            var courseToUpdate = await _context.Products
                                .FirstOrDefaultAsync(c => c.Id == id);
            if (await TryUpdateModelAsync<Product>(courseToUpdate, "",
              c => c.NameProduct, c => c.Price, c => c.Description,c => c.ImageFile, c => c.CategoryID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)

                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists, " + "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateDepartmentsDropDownList(courseToUpdate.CategoryID);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
