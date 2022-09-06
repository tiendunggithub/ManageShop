using ManageShop.Data;
using ManageShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ManageShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopContext _context;

        public HomeController(ILogger<HomeController> logger, ShopContext context)
        {
            _logger = logger;
            _context = context;  
        }

        public async Task<ActionResult> Index()
        {
            var viewModel = new Home();
            viewModel.ListCategory =  _context.Categories.ToList();
            viewModel.ListProduct = _context.Products.ToList();
            return View(viewModel);


            /*var shopContext = _context.Products.Include(p => p.Category);
            return View(await shopContext.ToListAsync());*/
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
