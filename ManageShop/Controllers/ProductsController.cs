using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManageShop.Data;
using ManageShop.Models;


namespace ManageShop.Views.Products
{
    public class ProductsController : Controller
    {
        private readonly ShopContext _context;

        public ProductsController(ShopContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            /*var shopContext = _context.Products.Include(p => p.Category).ToListAsync();*/
            return View(await PaginatedList<Product>.CreateAsync(_context.Products, pageNumber, 8));
        }

        // GET: Products/Details/5
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
        
    }
}
