using FDFVANLØSEEKSAMEN.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace FDFVANLØSEEKSAMEN.Controllers
{
    public class LoginController : Controller
    {
        private readonly easylotterik19_dk_db_fdfContext _context;
        public LoginController(easylotterik19_dk_db_fdfContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        // POST: ChildGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Brugernavn,Kodeord")] Bruger loginUser)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Brugers.FirstOrDefault(x => x.Brugernavn == loginUser.Brugernavn);
                if (user == null)
                {
                    ViewBag.message = "Brugernavn passer ikke, prøv igen..";
                    return View();
                }
                if (user.Kodeord != loginUser.Kodeord)
                {
                    ViewBag.message = "Kodeord passer ikke, prøv igen..";
                    return View();
                }
                HttpContext.Session.SetString("LoginUserId", user.BrugerId.ToString());
                HttpContext.Session.SetString("LoginUser", user.Brugernavn);
                HttpContext.Session.SetString("LoginUserType", user.Rolle);
                if (user.Rolle == Constants.UserLotteriBestyere)
                {
                    return RedirectToAction(nameof(Index), "BørneGruppe");
                }
                else if (user.Rolle == Constants.UserLeder)
                {
                    return RedirectToAction(nameof(Index), "LederBarn");
                }
                else if (user.Rolle == Constants.UserBestyrelse)
                {
                    return RedirectToAction(nameof(Index), "Bestyrelse");
                }
            }
            return View();
        }
    }
}
