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
    public class AdministratorsController : Controller
    {
        private readonly AcmeIncDbContext _context;

        public AdministratorsController(AcmeIncDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string inUsername, string inPassword)
        {
            if (ModelState.IsValid)
            {
                Admins oneUser = await _context.Administrators
                    .FirstOrDefaultAsync(u => u.UserSurname.Equals(inUsername) & u.UserPassword.Equals(inPassword));

                if (oneUser == null)
                {
                    ViewBag.userNotFound = "User not found, try again.";
                    return View();
                }

                HttpContext.Session.SetString("currentAdmin", oneUser.UserSurname);

                return RedirectToAction("adminIndex", "products");

            }
            return View();
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Administrators.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrator = await _context.Administrators
                .FirstOrDefaultAsync(m => m.UserSurname == id);
            if (administrator == null)
            {
                return NotFound();
            }

            return View(administrator);
        }


        public IActionResult Create()
        {
            return View();
        }

 
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserSurname,UserPassword")] Admins administrator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(administrator);
                await _context.SaveChangesAsync();


                HttpContext.Session.SetString("currentAdmin", administrator.UserSurname);

                return RedirectToAction("adminindex", "products");
            }
            return View(administrator);
        }


        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrator = await _context.Administrators.FindAsync(id);
            if (administrator == null)
            {
                return NotFound();
            }
            return View(administrator);
        }


        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserSurname,UserPassword")] Admins administrator)
        {
            if (id != administrator.UserSurname)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(administrator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministratorExists(administrator.UserSurname))
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
            return View(administrator);
        }


        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrator = await _context.Administrators
                .FirstOrDefaultAsync(m => m.UserSurname == id);
            if (administrator == null)
            {
                return NotFound();
            }

            return View(administrator);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var administrator = await _context.Administrators.FindAsync(id);
            _context.Administrators.Remove(administrator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdministratorExists(string id)
        {
            return _context.Administrators.Any(e => e.UserSurname == id);
        }
    }
}
