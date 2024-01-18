using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpicyX.DAL;
using SpicyX.Models;
using SpicyX.Utilities.Enums;

namespace SpicyX.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        public HomeController(AppDbContext db,RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            List<Chef> chefs = await _db.Chefs.Include(x=>x.Position).ToListAsync();
            return View(chefs);
        }
        public async Task<IActionResult> CreateRole()
        {
            foreach (var role in Enum.GetValues(typeof(UserRole)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                    await _roleManager.CreateAsync(new IdentityRole { Name=role.ToString()});
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
