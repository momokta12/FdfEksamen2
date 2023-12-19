using FDFVANLØSEEKSAMEN.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using FDFVANLØSEEKSAMEN.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace FDFVANLØSEEKSAMEN.Controllers
{
    public class BestyrelseController : Controller
    {
        private readonly easylotterik19_dk_db_fdfContext _context;

        public BestyrelseController(easylotterik19_dk_db_fdfContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var easylotterik19_dk_db_fdfContext = _context.Barns.Include(c => c.Bg).ToList();
            List<BørnDTO> Børn = new List<BørnDTO>();
            foreach (var barn in easylotterik19_dk_db_fdfContext)
            {
                var lodsolgt = _context.Betalings.Where(x => x.BarnId == barn.BarnId).Sum(x => x.AntalLod);
                Børn.Add(new BørnDTO()
                {
                    BNavn = barn.Bnavn,
                    BørneGruppe = barn.Bg.Bgnavn,
                    ModtagetLOD = barn.ModtagetLod,
                    RetuneretLOD = barn.RetuneretLod,
                    lodsolgt = lodsolgt
                });
            }
            if (Børn.Any(x => x.lodsolgt > 0))
            {
                ViewBag.message = "Supersælgeren er: " + Børn.OrderByDescending(o => o.lodsolgt).First().BNavn;
            }

            return View(Børn.OrderByDescending(o => o.lodsolgt));
        }
    }
}
