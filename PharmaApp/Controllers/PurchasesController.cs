using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmaApp.Models;

namespace PharmaApp.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly PharmaContext _context;

        public PurchasesController(PharmaContext context)
        {
            _context = context;
        }

        // GET: Purchases
        public async Task<IActionResult> Index()
        {
            var pharmaContext = _context.Purchase.Include(p => p.FK3).Include(p => p.FK4);
            return View(await pharmaContext.ToListAsync());
        }

        // GET: Purchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase
                .Include(p => p.FK3)
                .Include(p => p.FK4)
                .FirstOrDefaultAsync(m => m.purchase_id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // GET: Purchases/Create
        public IActionResult Create()
        {
            ViewData["med_id"] = new SelectList(_context.Medicines, "med_id", "med_id");
            ViewData["sup_id"] = new SelectList(_context.Suppliers, "sup_id", "sup_id");
            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("purchase_id,purchase_qty,purchase_amt,purchase_date,med_id,sup_id")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["med_id"] = new SelectList(_context.Medicines, "med_id", "med_id", purchase.med_id);
            ViewData["sup_id"] = new SelectList(_context.Suppliers, "sup_id", "sup_id", purchase.sup_id);
            return View(purchase);
        }

        // GET: Purchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }
            ViewData["med_id"] = new SelectList(_context.Medicines, "med_id", "med_id", purchase.med_id);
            ViewData["sup_id"] = new SelectList(_context.Suppliers, "sup_id", "sup_id", purchase.sup_id);
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("purchase_id,purchase_qty,purchase_amt,purchase_date,med_id,sup_id")] Purchase purchase)
        {
            if (id != purchase.purchase_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.purchase_id))
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
            ViewData["med_id"] = new SelectList(_context.Medicines, "med_id", "med_id", purchase.med_id);
            ViewData["sup_id"] = new SelectList(_context.Suppliers, "sup_id", "sup_id", purchase.sup_id);
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase
                .Include(p => p.FK3)
                .Include(p => p.FK4)
                .FirstOrDefaultAsync(m => m.purchase_id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchase = await _context.Purchase.FindAsync(id);
            _context.Purchase.Remove(purchase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseExists(int id)
        {
            return _context.Purchase.Any(e => e.purchase_id == id);
        }
    }
}
