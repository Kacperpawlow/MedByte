using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MedByte.Data;
using Medbyte.Models;

namespace Medbyte.Controllers
{
    [Authorize]
    public class RezerwacjaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RezerwacjaController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int tomografId, DateTime data, string imie, string nazwisko, string pesel)
        {
            var userId = _userManager.GetUserId(User);
            var rezerwacja = new Rezerwacja
            {
                TomografId = tomografId,
                Data = data,
                UserId = userId,
                Imie = imie,
                Nazwisko = nazwisko,
                Pesel = pesel
            };

            if (_context.Rezerwacje.Any(r => r.TomografId == tomografId && r.Data == data))
            {
                ModelState.AddModelError("data", "Wybrana data jest już zarezerwowana.");
                var tomograf = await _context.Tomografy.FindAsync(tomografId);
                var zarezerwowaneDaty = await _context.Rezerwacje
                    .Where(r => r.TomografId == tomografId)
                    .Select(r => r.Data)
                    .ToListAsync();

                ViewBag.ZarezerwowaneDaty = zarezerwowaneDaty;
                return View("Details", tomograf);
            }

            _context.Rezerwacje.Add(rezerwacja);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Tomografy", new { id = tomografId });
        }

        [HttpGet]
        public async Task<IActionResult> MojeTerminy()
        {
            var userId = _userManager.GetUserId(User);
            var rezerwacje = await _context.Rezerwacje
                .Include(r => r.Tomograf)
                .Where(r => r.UserId == userId)
                .ToListAsync();

            return View(rezerwacje);
        }

        [HttpPost]
        public async Task<IActionResult> Odwolaj(int id)
        {
            var rezerwacja = await _context.Rezerwacje.FindAsync(id);
            if (rezerwacja == null || rezerwacja.UserId != _userManager.GetUserId(User))
            {
                return Forbid();
            }

            _context.Rezerwacje.Remove(rezerwacja);
            await _context.SaveChangesAsync();

            return RedirectToAction("MojeTerminy");
        }
    }
}
