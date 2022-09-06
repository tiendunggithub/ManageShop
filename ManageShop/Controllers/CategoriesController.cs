using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManageShop.Data;
using ManageShop.Models;

namespace ManageShop.Views.Categories
{
    public class CategoriesController : Controller
    {
        private readonly ShopContext _context;

        public CategoriesController(ShopContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }
        public async Task<ActionResult> ProductCate(int Id, int pageNumber = 1)
        {
            var productcate = await _context.Products.Where(m => m.CategoryID == Id).ToListAsync();
            productcate =  await PaginatedList<Product>.CreateAsync(_context.Products, pageNumber, 4);
            return View(productcate);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryID == id);
        }
    }
}
