using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bernardi.Luca._5H.Ecommerce.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Bernardi.Luca._5H.Ecommerce.Controllers
{
    public class UtentiController : Controller
    {
        private readonly dbContext _context;

        public UtentiController(dbContext context)
        {
            _context = context;
        }
        public IActionResult LogOut()
        {
            string? nickname = HttpContext.Session.GetString("Login");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult LogIn()
        {
            return View();
        }
         [HttpPost]
        public IActionResult LogIn(Utenti utente)
        {
            // Cerca se esiste l'utente nel DB
            var user = _context.Utenti.FirstOrDefault(u => u.Nickname == utente.Nickname && u.Password == utente.Password);

            if (user != null)
            {
                // L'utente esiste e quindi parte la sessione
                HttpContext.Session.SetString("Login", user.Nickname);
                HttpContext.Session.SetString("IsAdmin", user.Admin.ToString());
                HttpContext.Session.SetInt32("Id", user.Id);

                return RedirectToAction("Index", "Home");
            }

            // Se non va il log in ti rimanda alla stessa pagina
            ModelState.AddModelError(string.Empty, "Invalid Nickname or Password");
            return View(utente);
        }
        // GET: Utenti
        public async Task<IActionResult> Index()
        {
            return View(await _context.Utenti.ToListAsync());
        }

        // GET: Utenti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utenti = await _context.Utenti
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utenti == null)
            {
                return NotFound();
            }

            return View(utenti);
        }

        // GET: Utenti/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Utenti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Cognome,Nickname,DataNascita,Email,Numero,Password,Admin")] Utenti utenti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utenti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(LogIn));
            }
            return View(utenti);
        }

        // GET: Utenti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utenti = await _context.Utenti.FindAsync(id);
            if (utenti == null)
            {
                return NotFound();
            }
            return View(utenti);
        }

        // POST: Utenti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Cognome,Nickname,DataNascita,Email,Numero,Password,Admin")] Utenti utenti)
        {
            if (id != utenti.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utenti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtentiExists(utenti.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = utenti.Id });
            }
            return View(utenti);
        }

        // GET: Utenti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utenti = await _context.Utenti
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utenti == null)
            {
                return NotFound();
            }

            return View(utenti);
        }

        // POST: Utenti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utenti = await _context.Utenti.FindAsync(id);
            if (utenti != null)
            {
                _context.Utenti.Remove(utenti);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool UtentiExists(int id)
        {
            return _context.Utenti.Any(e => e.Id == id);
        }
    }
}