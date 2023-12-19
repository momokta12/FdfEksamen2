using FDFVANLØSEEKSAMEN.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FDFVANLØSEEKSAMEN.Controllers
{
    public class LederBarnController : Controller
    { 
    private readonly easylotterik19_dk_db_fdfContext _context;

    public LederBarnController(easylotterik19_dk_db_fdfContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var brugereID = HttpContext.Session.GetString("LoginUserId");
        Int32.TryParse(brugereID ?? "0", out var lederId);
        var easylotterik19_dk_db_fdfContext = _context.Barns.Where(x => x.Bg.LederId == lederId).Include(c => c.Bg);
        return View(await easylotterik19_dk_db_fdfContext.ToListAsync());
    }

    // GET: Barn/Edit/5
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

        // POST: Barn/Edit/5
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
        ViewData["Bgid"] = new SelectList(_context.BørneGruppes, "Bgid", "Bgnavn", barn.Bgid);
        return View(barn);
    }
    private bool BarnExsist(int id)
    {
        return (_context.Barns?.Any(e => e.BarnId == id)).GetValueOrDefault();
    }
}
}
