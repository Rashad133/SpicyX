using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpicyX.Areas.Admin.ViewModels.Settings;
using SpicyX.DAL;
using SpicyX.Models;

namespace SpicyX.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingsController : Controller
    {
        private readonly AppDbContext _db;
        public SettingsController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Settings> settings = await _db.Settings.ToListAsync();
            return View(settings);
        }

        public async Task<IActionResult> Update(int id)
        {
            if(id<=0) return BadRequest();
            Settings settings = await _db.Settings.FirstOrDefaultAsync(x => x.Id == id);
            if(settings==null) return NotFound();

            UpdateSettingsVM update = new UpdateSettingsVM { Key=settings.Key,Value=settings.Value };
            return View(update);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdateSettingsVM update)
        {
            if(!ModelState.IsValid) return View(update);
            Settings settings = await _db.Settings.FirstOrDefaultAsync(x=>x.Id == id);
            if(settings==null) return NotFound();

            bool result= await _db.Settings.AnyAsync(x=>x.Key.Trim().ToLower() == update.Key.ToLower() && x.Id!=id);
            if (result)
            {
                ModelState.AddModelError("Name","exists");
                return View(update);
            }
            settings.Key= update.Key;
            settings.Value=update.Value;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
