using FDFVANLØSEEKSAMEN.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FDFVANLØSEEKSAMEN.Controllers
{
    public class BetalingsController : Controller
    {
        private readonly easylotterik19_dk_db_fdfContext _context;

        public BetalingsController(easylotterik19_dk_db_fdfContext context)
        {
            _context = context;
        }

        // GET: Betalinger
        public async Task<IActionResult> Index()
        {
            var easylotterik19_dk_db_fdfContext = _context.Betalings.Include(p => p.Barn);
            return View(await easylotterik19_dk_db_fdfContext.ToListAsync());
        }

        // GET: Betalinger/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Betalings == null)
            {
                return NotFound();
            }

            var betaling = await _context.Betalings
                .Include(p => p.Barn )
                .FirstOrDefaultAsync(m => m.BetalingsId == id);
            if (betaling == null)
            {
                return NotFound();
            }

            return View(betaling);
        }

        // GET: Betalinger/Create
        public IActionResult Create()
        {
            ViewData["BarnId"] = new SelectList(_context.Barns, "BarnId", "Bnavn");
            return View();
        }

        // POST: Betalinger/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BetalingsId,BetalingsDato,Beløbet,AntalLod,BarnId")] Betaling betaling)
        {
            if (ModelState.IsValid)
            {
                _context.Add(betaling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BarnId"] = new SelectList(_context.Barns, "BarnId", "Bnavn", betaling.BarnId);
            return View(betaling);
        }

        // GET: Betalinger/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Betalings == null)
            {
                return NotFound();
            }

            var betaling = await _context.Betalings.FindAsync(id);
            if (betaling == null)
            {
                return NotFound();
            }
            ViewData["BarnId"] = new SelectList(_context.Barns, "BarnId", "Bnavn", betaling.BarnId);
            return View(betaling);
        }

        // POST: Betalinger/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BetalingsId,BetalingsDato,Beløbet,AntalLod,BarnId")] Betaling betaling)
        {
            if (id != betaling.BetalingsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(betaling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BetalingExists(betaling.BetalingsId))
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
            ViewData["BarnId"] = new SelectList(_context.Barns, "BarnId", "Bnavn", betaling.BarnId);
            return View(betaling);
        }

        // GET: Betalinger/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Betalings == null)
            {
                return NotFound();
            }

            var betaling = await _context.Betalings
                .Include(p => p.Barn)
                .FirstOrDefaultAsync(m => m.BetalingsId == id);
            if (betaling == null)
            {
                return NotFound();
            }

            return View(betaling);
        }

        // POST: Betalinger/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Betalings == null)
            {
                return Problem("Entity set 'easylotterik19_dk_db_fdfContext.Betaling'  is null.");
            }
            var betaling = await _context.Betalings.FindAsync(id);
            if (betaling != null)
            {
                _context.Betalings.Remove(betaling);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BetalingExists(int id)
        {
            return (_context.Betalings?.Any(e => e.BetalingsId == id)).GetValueOrDefault();
        }
    }
}
