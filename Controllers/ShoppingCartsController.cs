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
    public class ShoppingCartsController : Controller
    {
        private readonly AcmeIncDbContext _context;

        public ShoppingCartsController(AcmeIncDbContext context)
        {
            _context = context;
        }

      
        public async Task<IActionResult> Index()
        {
            string username = HttpContext.Session.GetString("currentUser");

            if (username == null)
            {
                return RedirectToAction("login", "CustomerUsers");
            }

            var AcmeIncDbContext = _context.ShoppingCarts.Where(u => u.Username.Equals(username));

            return View(await AcmeIncDbContext.ToListAsync());
        }

        public async Task<IActionResult> adminIndex()
        {
            string adminUsername = HttpContext.Session.GetString("currentAdmin");

            if(adminUsername == null){
                return RedirectToAction("login", "Administrators");
            }   

                var AcmeIncDbContext = _context.ShoppingCarts.Include(s => s.Pro).Include(s => s.UsernameNavigation);
                return View(await AcmeIncDbContext.ToListAsync());
        }
        


        public IActionResult Create()
        {
            ViewData["Pro_Id"] = new SelectList(_context.Products, "Pro_Id", "Pro_Id");
            ViewData["Username"] = new SelectList(_context.CustomerUsers, "Username", "Username");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Pro_Id")] Cart shoppingCart)
        {
                
            if (ModelState.IsValid)
            {
                _context.Add(shoppingCart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Proid"] = new SelectList(_context.Products, "Pro_Id", "Pro_Id", shoppingCart.Proid);
            ViewData["Username"] = new SelectList(_context.CustomerUsers, "Username", "Username", shoppingCart.Username);
            return View(shoppingCart);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create2([Bind("Id,Username,Proid")] Cart shoppingCart)
        {


            if (ModelState.IsValid)
            {
                _context.Add(shoppingCart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Proid"] = new SelectList(_context.Products, "Pro_Id", "Pro_Id", shoppingCart.Proid);
            ViewData["Username"] = new SelectList(_context.CustomerUsers, "Username", "Username", shoppingCart.Username);
            return View(shoppingCart);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCarts
                .Include(s => s.Pro)
                .Include(s => s.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
            if (shoppingCart == null)
            {
                return NotFound();
            }
            ViewData["Pro_Id"] = new SelectList(_context.Products, "Pro_Id", "Pro_Id", shoppingCart.Proid);
            ViewData["Username"] = new SelectList(_context.CustomerUsers, "Username", "Username", shoppingCart.Username);
            return View(shoppingCart);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
            _context.ShoppingCarts.Remove(shoppingCart);
            await _context.SaveChangesAsync();

            string admin = HttpContext.Session.GetString("currentAdmin");
            string username = HttpContext.Session.GetString("currentUser");

            if (admin != null)
            {
                return RedirectToAction("adminindex", "shoppingcarts");
            }
            else if (username != null)
            {
                return RedirectToAction("index", "shoppingcarts");
            }

            return RedirectToAction("login", "customerUsers");
        }

        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Proid")] Cart shoppingCart)
        {
            if (id != shoppingCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingCartExists(shoppingCart.Id))
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
            ViewData["Pro_Id"] = new SelectList(_context.Products, "Pro_Id", "Pro_Id", shoppingCart.Proid);
            ViewData["Username"] = new SelectList(_context.CustomerUsers, "Username", "Username", shoppingCart.Username);
            return View(shoppingCart);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCarts
                .Include(s => s.Pro)
                .Include(s => s.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

   

        private bool ShoppingCartExists(int id)
        {
            return _context.ShoppingCarts.Any(e => e.Id == id);
        }
    }
}
