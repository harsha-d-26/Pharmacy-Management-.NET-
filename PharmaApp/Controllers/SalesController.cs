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
    public class SalesController : Controller
    {
        private readonly PharmaContext _context;

        public SalesController(PharmaContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            var pharmaContext = _context.Sales.Include(s => s.FK1).Include(s => s.FK2);
            return View(await pharmaContext.ToListAsync());
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales
                .Include(s => s.FK1)
                .Include(s => s.FK2)
                .FirstOrDefaultAsync(m => m.sale_id == id);
            if (sales == null)
            {
                return NotFound();
            }

            return View(sales);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            ViewData["med_id"] = new SelectList(_context.Medicines, "med_id", "med_id");
            ViewData["user_id"] = new SelectList(_context.Users, "user_id", "user_id");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("sale_id,date_time,total_amt,med_id,user_id")] Sales sales)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sales);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["med_id"] = new SelectList(_context.Medicines, "med_id", "med_id", sales.med_id);
            ViewData["user_id"] = new SelectList(_context.Users, "user_id", "user_id", sales.user_id);
            return View(sales);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales.FindAsync(id);
            if (sales == null)
            {
                return NotFound();
            }
            ViewData["med_id"] = new SelectList(_context.Medicines, "med_id", "med_id", sales.med_id);
            ViewData["user_id"] = new SelectList(_context.Users, "user_id", "user_id", sales.user_id);
            return View(sales);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("sale_id,date_time,total_amt,med_id,user_id")] Sales sales)
        {
            if (id != sales.sale_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sales);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesExists(sales.sale_id))
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
            ViewData["med_id"] = new SelectList(_context.Medicines, "med_id", "med_id", sales.med_id);
            ViewData["user_id"] = new SelectList(_context.Users, "user_id", "user_id", sales.user_id);
            return View(sales);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales
                .Include(s => s.FK1)
                .Include(s => s.FK2)
                .FirstOrDefaultAsync(m => m.sale_id == id);
            if (sales == null)
            {
                return NotFound();
            }

            return View(sales);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sales = await _context.Sales.FindAsync(id);
            _context.Sales.Remove(sales);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesExists(int id)
        {
            return _context.Sales.Any(e => e.sale_id == id);
        }
    }
}
