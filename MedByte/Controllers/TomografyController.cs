using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Medbyte.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Identity;
using MedByte.Data;

namespace Medbyte.Controllers
{
    [Authorize]
    public class TomografyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TomografyController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var currentUserId = _userManager.GetUserId(User);
            var tomografy = await _context.Tomografy.ToListAsync();
            ViewBag.CurrentUserId = currentUserId;
            return View(tomografy);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tomograf = await _context.Tomografy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tomograf == null)
            {
                return NotFound();
            }

            var zarezerwowaneDaty = await _context.Rezerwacje
                .Where(r => r.TomografId == tomograf.Id)
                .Select(r => r.Data)
                .ToListAsync();

            ViewBag.ZarezerwowaneDaty = zarezerwowaneDaty;

            return View(tomograf);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Opis,Telefon,Ulica,Miasto,KodPocztowy")] Tomograf tomograf, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    tomograf.ImagePath = "/images/" + imageFile.FileName;
                }

                tomograf.UserId = _userManager.GetUserId(User);
                _context.Add(tomograf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tomograf);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tomograf = await _context.Tomografy.FindAsync(id);
            if (tomograf == null || tomograf.UserId != _userManager.GetUserId(User))
            {
                return Forbid();
            }
            return View(tomograf);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Opis,Telefon,Ulica,Miasto,KodPocztowy")] Tomograf tomograf, IFormFile imageFile)
        {
            if (id != tomograf.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }
                        tomograf.ImagePath = "/images/" + imageFile.FileName;
                    }

                    tomograf.UserId = _userManager.GetUserId(User);
                    _context.Update(tomograf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TomografExists(tomograf.Id))
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
            return View(tomograf);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tomograf = await _context.Tomografy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tomograf == null || tomograf.UserId != _userManager.GetUserId(User))
            {
                return Forbid();
            }

            return View(tomograf);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tomograf = await _context.Tomografy.FindAsync(id);
            if (tomograf.UserId != _userManager.GetUserId(User))
            {
                return Forbid();
            }
            _context.Tomografy.Remove(tomograf);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TomografExists(int id)
        {
            return _context.Tomografy.Any(e => e.Id == id);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetTomografy()
        {
            var tomografy = await _context.Tomografy.ToListAsync();
            return Json(tomografy);
        }
    }
}
