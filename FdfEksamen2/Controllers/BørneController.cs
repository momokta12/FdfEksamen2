using FDFVANLØSEEKSAMEN.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FDFVANLØSEEKSAMEN.Controllers
{
    public class BørneController : Controller
  { 
    private readonly easylotterik19_dk_db_fdfContext _context;

    public BørneController(easylotterik19_dk_db_fdfContext context)
    {
        _context = context;
    }

    // GET: Børn
    public async Task<IActionResult> Index()
    {
        var easylotterik19_dk_db_fdfContext = _context.Barns.Include(c => c.Bg);
        return View(await easylotterik19_dk_db_fdfContext.ToListAsync());
    }

        // GET: Børn/Details/5
        public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Barns == null)
        {
            return NotFound();
        }

        var barn = await _context.Barns
            .Include(c => c.Bg)
            .FirstOrDefaultAsync(m => m.BarnId == id);
        if (barn == null)
        {
            return NotFound();
        }

        return View(barn);
    }

        // GET: Børn/Create
        public IActionResult Create()
    {
        ViewData["Bgid"] = new SelectList(_context.BørneGruppes, "Bgid", "Bgnavn");
        return View();
    }

        // POST: Børn/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("BarnId,Bnavn,Bgid,ModtagetLod,RetuneretLod")] Barn barn)
    {
        if (ModelState.IsValid)
        {
            _context.Add(barn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["Bgid"] = new SelectList(_context.BørneGruppes, "Bgid", "Bgnavn", barn.BarnId);
        return View(barn);
    }

        // GET: Børn/Edit/5
        public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Barns == null)
        {
            return NotFound();
        }

        var barn = await _context.Barns.FindAsync(id);
        if (barn == null)
        {
            return NotFound();
        }
        ViewData["Bgid"] = new SelectList(_context.BørneGruppes, "Bgid", "Bgnavn", barn.Bgid);
        return View(barn);
    }

        // POST: Børn/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("BarnId,Bnavn,Bgid,ModtagetLod,RetuneretLod")] Barn barn)
        {
        if (id != barn.BarnId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(barn);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarnExsist(barn.BarnId))
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
        ViewData["Bgid"] = new SelectList(_context.BørneGruppes, "Bgid", "Bgnavn", barn.BarnId);
        return View(barn);
    }

        // GET: Børn/Delete/5
        public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Barns == null)
        {
            return NotFound();
        }

        var barn = await _context.Barns
            .Include(c => c.Bg)
            .FirstOrDefaultAsync(m => m.BarnId == id);
        if (barn == null)
        {
            return NotFound();
        }

        return View(barn);
    }

        // POST: Børn/Delete/5
        [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Barns == null)
        {
            return Problem("Entity set 'easylotterik19_dk_db_fdfContext.Børn'  is null.");
        }
        var barn = await _context.Barns.FindAsync(id);
        if (barn != null)
        {
            _context.Barns.Remove(barn);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BarnExsist(int id)
    {
        return (_context.Barns?.Any(e => e.BarnId == id)).GetValueOrDefault();
    }
}
}
