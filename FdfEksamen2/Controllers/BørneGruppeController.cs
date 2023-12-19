using FDFVANLØSEEKSAMEN.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FDFVANLØSEEKSAMEN.Controllers
{
    public class BørneGruppeController : Controller
    {
            private readonly easylotterik19_dk_db_fdfContext _context;

            public BørneGruppeController(easylotterik19_dk_db_fdfContext context)
            {
                _context = context;
            }

        // GET: Børnegrupper
        public async Task<IActionResult> Index()
            {
                var easylotterik19_dk_db_fdfContext = _context.BørneGruppes.Include(c => c.Leder);
                return View(await easylotterik19_dk_db_fdfContext.ToListAsync());
            }

        // GET: Børnegrupper/Details/5
        public async Task<IActionResult> Details(int? id)
            {
                if (id == null || _context.BørneGruppes == null)
                {
                    return NotFound();
                }

                var childGroup = await _context.BørneGruppes
                    .Include(c => c.Leder)
                    .FirstOrDefaultAsync(m => m.Bgid == id);
                if (childGroup == null)
                {
                    return NotFound();
                }

                return View(childGroup);
            }

        // GET: Børnegrupper/Create
        public IActionResult Create()
            {
                var list = new List<SelectListItem>();
                var userList = _context.Brugers.Where(x => x.Rolle == "Leder").ToList();
                foreach (var user in userList)
                {
                    list.Add(new SelectListItem { Value = user.BrugerId.ToString(), Text = user.Brugernavn });
                }
            //ViewData["LederId"] = new SelectList(_context.Users, "BrugerID", "BrugerID");
            ViewBag.LederId = list;
                return View();
            }

        // POST: Børnegrupper/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("BGID,BGNavn,LederID,ModtagetLod,RetuneretLod")] BørneGruppe børneGruppe)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(børneGruppe);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["LederID"] = new SelectList(_context.Brugers.Where(x => x.Rolle == "Leder"), "BrugerID", "Brugernavn", børneGruppe.LederId);
                return View(børneGruppe);
            }

        // GET: Børnegrupper/Edit/5
        public async Task<IActionResult> Edit(int? id)
            {
                if (id == null || _context.BørneGruppes == null)
                {
                    return NotFound();
                }

                var børneGruppe = await _context.BørneGruppes.FindAsync(id);
                if (børneGruppe == null)
                {
                    return NotFound();
                }
                ViewData["LeaderId"] = new SelectList(_context.Brugers.Where(x => x.Rolle == "Leder"), "BrugerID", "Brugernavn", børneGruppe.LederId);
                return View(børneGruppe);
            }

        // POST: Børnegrupper/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("BGID,BGNavn,LederID,ModtagetLod,RetuneretLOD")] BørneGruppe børneGruppe)
            {
                if (id != børneGruppe.Bgid)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(børneGruppe);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BørneGruppeExists(børneGruppe.Bgid))
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
                ViewData["LederId"] = new SelectList(_context.Brugers, "BrugerID", "Brugernavn", børneGruppe.LederId);
                return View(børneGruppe);
            }

        // GET: Børnegrupper/Delete/5
        public async Task<IActionResult> Delete(int? id)
            {
                if (id == null || _context.BørneGruppes == null)
                {
                    return NotFound();
                }

                var børneGruppe = await _context.BørneGruppes
                    .Include(c => c.Leder)
                    .FirstOrDefaultAsync(m => m.Bgid == id);
                if (børneGruppe == null)
                {
                    return NotFound();
                }

                return View(børneGruppe);
            }

        // POST: Børnegrupper/Delete/5
        [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                if (_context.BørneGruppes == null)
                {
                    return Problem("Entity set 'easylotterik19_dk_db_fdfContext.Børnegrupper'  is null.");
                }
                var børneGruppe = await _context.BørneGruppes.FindAsync(id);
                if (børneGruppe != null)
                {
                    _context.BørneGruppes.Remove(børneGruppe);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool BørneGruppeExists(int id)
            {
                return (_context.BørneGruppes?.Any(e => e.Bgid == id)).GetValueOrDefault();
            }
        }
    }
