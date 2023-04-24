using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROG7311_Task2.Models.AcmeIncTask2db;

namespace PROG7311_Task2.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AcmeIncDbContext _context;

        public ProductsController(AcmeIncDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }
        
        public async Task<IActionResult> adminIndex()
        {
            string adminUsername = HttpContext.Session.GetString("currentAdmin");

            if (adminUsername == null)
            {
                return RedirectToAction("login", "customerUsers");
            }

            return View(await _context.Products.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Proid == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }


        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Pro_Id,ProName,ProPrice,ProDescription")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("adminindex", "products");
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Pro_Id,ProName,ProPrice,ProDescription")] Product product)
        {
            if (id != product.Proid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Proid))
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
            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Proid == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


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
            return _context.Products.Any(e => e.Proid == id);
        }

        [HttpPost]
        public async Task<IActionResult> index(string productName)
        {

            var product =  _context.Products.Where(u => u.ProName.Contains(productName));

            if (product.ToList().Count().Equals(0))
            {
                ViewBag.productNotFound = "Product Not Found!";
                return View(await _context.Products.ToListAsync());
            }
            
            return View(await product.ToListAsync());

        }

        public async Task<IActionResult> cart(int? id)
        {
            string username = HttpContext.Session.GetString("currentUser");
    
            if(username == null)
            {
                return RedirectToAction("login", "customerUsers");
            }

            ShoppingCart shoppingCart = new ShoppingCart(username, id);

            _context.Add(shoppingCart);
            await _context.SaveChangesAsync();

            return RedirectToAction("index", "ShoppingCarts");

        }


    }
}
