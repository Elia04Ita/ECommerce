using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bernardi.Luca._5H.Ecommerce.Models;

namespace Bernardi.Luca._5H.Ecommerce.Controllers
{
    public class ProdottiController : Controller
    {
        private readonly dbContext _context;

        public ProdottiController(dbContext context)
        {
            _context = context;
        }

        // GET: Prodotti
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prodotti.ToListAsync());
        }

        public async Task<IActionResult> Aggiungi(int id)
        {
            // Estrai l'ID dell'utente loggato dalla sessione
            int userId = HttpContext.Session.GetInt32("Id") ?? 0;

            // Trova il prodotto corrispondente all'ID
            var prodotto = await _context.Prodotti.FindAsync(id);

            if (prodotto == null)
            {
                return NotFound();
            }

            // Cerca se il prodotto è già nel carrello dell'utente
            var carrelloItem = await _context.Carrello.FirstOrDefaultAsync(c => c.Utente == userId && c.Prodotto == id);

            if (carrelloItem != null)
            {
                // Se il prodotto è già nel carrello, aggiorna la quantità
                carrelloItem.Quantita++;
            }
            else
            {
                // Altrimenti, aggiungi un nuovo elemento al carrello
                var newCartItem = new Carrello
                {
                    Prodotto = id,
                    Utente = userId,
                    Quantita = 1
                };

                _context.Carrello.Add(newCartItem);
            }

            await _context.SaveChangesAsync(); // Salva le modifiche nel database del carrello
            return RedirectToAction(nameof(Index));
        }



        // GET: Prodotti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotti = await _context.Prodotti
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prodotti == null)
            {
                return NotFound();
            }

            return View(prodotti);
        }

        // GET: Prodotti/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prodotti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ReleaseDate,Description,Price,ImageFileName")] Prodotti prodotti)
        {
            if (ModelState.IsValid)
            {
                // Additional model validation
                if (prodotti.ReleaseDate < DateTime.Today)
                {
                    ModelState.AddModelError(nameof(prodotti.ReleaseDate), "La data di rilascio deve essere per questo giorno o i successivi.");
                    return View(prodotti);
                }
                _context.Add(prodotti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prodotti);
        }

        // GET: Prodotti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotti = await _context.Prodotti.FindAsync(id);
            if (prodotti == null)
            {
                return NotFound();
            }
            return View(prodotti);
        }

        // POST: Prodotti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ReleaseDate,Description,Price,ImageFileName")] Prodotti prodotti)
        {
            if (id != prodotti.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prodotti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdottiExists(prodotti.Id))
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
            return View(prodotti);
        }

        // GET: Prodotti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotti = await _context.Prodotti
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prodotti == null)
            {
                return NotFound();
            }

            return View(prodotti);
        }

        // POST: Prodotti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prodotti = await _context.Prodotti.FindAsync(id);
            if (prodotti != null)
            {
                _context.Prodotti.Remove(prodotti);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdottiExists(int id)
        {
            return _context.Prodotti.Any(e => e.Id == id);
        }
    }
}
