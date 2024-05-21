using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bernardi.Luca._5H.Ecommerce.Models;
using Microsoft.AspNetCore.Http;

namespace Bernardi.Luca._5H.Ecommerce.Controllers
{
    public class CarrelloController : Controller
    {
        private readonly dbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarrelloController(dbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Carrello
        public async Task<IActionResult> Index()
        {
            // Estrai l'ID dell'utente loggato
            int userId = HttpContext.Session.GetInt32("Id") ?? 0;
            
            // Filtra il carrello per l'utente loggato e lo unisce con i prodotti
            var carrello = await _context.Carrello
                .Where(c => c.Utente == userId) // Filtra per l'utente loggato
                .Join(_context.Prodotti, // Unisciti con la tabella dei prodotti
                      c => c.Prodotto, 
                      p => p.Id, 
                      (c, p) => new { Carrello = c, Prodotto = p }) // Seleziona il carrello e il prodotto
                .ToListAsync();

            return View(carrello);
        }

        // GET: Carrello/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrello = await _context.Carrello
                .FirstOrDefaultAsync(m => m.id == id);
            if (carrello == null)
            {
                return NotFound();
            }

            return View(carrello);
        }

        // GET: Carrello/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carrello/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Prodotto,Utente,Quantita")] Carrello carrello)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carrello);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carrello);
        }

        // GET: Carrello/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrello = await _context.Carrello.FindAsync(id);
            if (carrello == null)
            {
                return NotFound();
            }
            return View(carrello);
        }

        // POST: Carrello/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Prodotto,Utente,Quantita")] Carrello carrello)
        {
            if (id != carrello.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrello);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarrelloExists(carrello.id))
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
            return View(carrello);
        }

        // GET: Carrello/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrello = await _context.Carrello
                .FirstOrDefaultAsync(m => m.id == id);
            if (carrello == null)
            {
                return NotFound();
            }

            // Ottenere il nome del prodotto associato al carrello
            var prodotto = await _context.Prodotti.FirstOrDefaultAsync(p => p.Id == carrello.Prodotto);
            if (prodotto == null)
            {
                return NotFound();
            }

            // Passare il nome del prodotto alla vista invece dell'ID del prodotto
            ViewData["ProductName"] = prodotto.Name;

            return View(carrello);
        }

        // POST: Carrello/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carrello = await _context.Carrello.FindAsync(id);
            if (carrello != null)
            {
                _context.Carrello.Remove(carrello);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Carrello/DeleteAll
        public async Task<IActionResult> DeleteAll()
        {
            try
            {
                // Ottieni tutti i record nella tabella Carrello
                var carrelloItems = await _context.Carrello.ToListAsync();

                // Rimuovi tutti i record
                _context.Carrello.RemoveRange(carrelloItems);

                // Salva le modifiche nel database
                await _context.SaveChangesAsync();

                // Ritorna alla vista Index o a un'altra vista desiderata
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Gestisci eventuali eccezioni
                return RedirectToAction("Error", "Home"); // Reindirizza a una vista di errore o gestisci in altro modo
            }
        }

        // POST: Carrello/Ordine66
        [HttpPost]
        public async Task<IActionResult> Ordine66()
        {
            try
            {
                // Estrai l'ID dell'utente loggato
                int userId = HttpContext.Session.GetInt32("Id") ?? 0;

                // Ottieni tutti i record nella tabella Carrello per l'utente loggato
                var carrelloItems = await _context.Carrello
                    .Where(c => c.Utente == userId)
                    .ToListAsync();

                // Rimuovi tutti i record
                _context.Carrello.RemoveRange(carrelloItems);

                // Salva le modifiche nel database
                await _context.SaveChangesAsync();

                // Ritorna alla vista Index o a un'altra vista desiderata
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Gestisci eventuali eccezioni
                return RedirectToAction("Error", "Home"); // Reindirizza a una vista di errore o gestisci in altro modo
            }
        }

        private bool CarrelloExists(int id)
        {
            return _context.Carrello.Any(e => e.id == id);
        }
    }
}
