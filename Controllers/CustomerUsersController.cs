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
    public class CustomerUsersController : Controller
    {
        private readonly AcmeIncDbContext _context;

        public CustomerUsersController(AcmeIncDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                User oneUser = await _context.CustomerUsers
                    .FirstOrDefaultAsync(u => u.Username.Equals(username) & u.UserPassword.Equals(password));

                if (oneUser == null)
                {
                    ViewBag.userNotFound = "User not found, try again.";
                    return View();
                }

                HttpContext.Session.SetString("currentUser", oneUser.Username);

                return RedirectToAction("index", "products");

            }
            return View();
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomerUsers.ToListAsync());
        }


        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerUser = await _context.CustomerUsers
                .FirstOrDefaultAsync(m => m.Username == id);
            if (customerUser == null)
            {
                return NotFound();
            }

            return View(customerUser);
        }


        public IActionResult Create()
        {
            return View();
        }


        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,UserFirstname,UserLastname,UserPassword")] User customerUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerUser);
                await _context.SaveChangesAsync();

                HttpContext.Session.SetString("currentUser", customerUser.Username);

                return RedirectToAction("index", "products");
            }
            return View(customerUser);
        }


        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerUser = await _context.CustomerUsers.FindAsync(id);
            if (customerUser == null)
            {
                return NotFound();
            }
            return View(customerUser);
        }


        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Username,UserFirstname,UserLastname,UserPassword")] User customerUser)
        {
            if (id != customerUser.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerUserExists(customerUser.Username))
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
            return View(customerUser);
        }


        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerUser = await _context.CustomerUsers
                .FirstOrDefaultAsync(m => m.Username == id);
            if (customerUser == null)
            {
                return NotFound();
            }

            return View(customerUser);
        }

  
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customerUser = await _context.CustomerUsers.FindAsync(id);
            _context.CustomerUsers.Remove(customerUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerUserExists(string id)
        {
            return _context.CustomerUsers.Any(e => e.Username == id);
        }
    }
}
