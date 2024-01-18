using Be.Areas.Admin.ViewModels;
using Be.Areas.Admin.ViewModels.Chef;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpicyX.DAL;
using SpicyX.Models;
using SpicyX.Utilities.Extentions;

namespace SpicyX.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChefController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public ChefController(AppDbContext db,IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Chef> chefs =await _db.Chefs.Include(x=>x.Position).ToListAsync();
            return View(chefs);
        }

        public async Task<IActionResult> Create()
        {
            CreateChefVM create = new CreateChefVM
            {
                Positions= await _db.Positions.ToListAsync(),
            };
            return View(create);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateChefVM create)
        {
            if (!ModelState.IsValid)
            {
                create.Positions= await _db.Positions.ToListAsync();
                return View(create);
            }
            bool result = await _db.Chefs.AnyAsync(x=>x.Name.Trim().ToLower()==create.Name.Trim().ToLower());
            if (result)
            {
                create.Positions = await _db.Positions.ToListAsync();
                ModelState.AddModelError("Name","is exist");
                return View(create);
            }
            if (!create.Photo.ValidateType())
            {
                create.Positions = await _db.Positions.ToListAsync();
                ModelState.AddModelError("Photo","not valid");
                return View(create);
            }
            if (!create.Photo.ValidateSize(10))
            {
                create.Positions=await _db.Positions.ToListAsync();
                ModelState.AddModelError("Photo","max 10mb");
                return View(create);
            }

            Chef chef = new Chef
            {
                Name = create.Name,
                Image = await create.Photo.CreateFile(_env.WebRootPath,"assets","img"),
                TwitLink= create.TwitLink,
                FaceLink= create.FaceLink,
                LinkedLink= create.LinkedLink,
                GoogleLink= create.GoogleLink,
                PositionId= create.PositionId,
            };
            await _db.Chefs.AddAsync(chef);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Chef chef=await _db.Chefs.FirstOrDefaultAsync(x=>x.Id == id);
            if(chef==null) return NotFound();

            UpdateChefVM update = new UpdateChefVM
            {
                Name= chef.Name,
                Image= chef.Image,
                PositionId= chef.PositionId,
                Positions=await _db.Positions.ToListAsync(),
                TwitLink = chef.TwitLink,
                FaceLink= chef.FaceLink,
                LinkedLink= chef.LinkedLink,
                GoogleLink= chef.GoogleLink,
            };
            return View(update);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdateChefVM update)
        {
            if(!ModelState.IsValid)
            {
                update.Positions = await _db.Positions.ToListAsync();
                return View(update);
            }
            Chef chef = await _db.Chefs.FirstOrDefaultAsync(x=> x.Id == id);
            if(chef==null) return NotFound();

            bool result = await _db.Chefs.AnyAsync(x=>x.Name.Trim().ToLower()==update.Name.Trim().ToLower() && x.Id!=id);
            if(result)
            {
                update.Positions= await _db.Positions.ToListAsync();
                ModelState.AddModelError("Name","is exists");
                return View(update);
            }
            if (update.Photo is not null)
            {
                if (!update.Photo.ValidateType())
                {
                    update.Positions = await _db.Positions.ToListAsync();
                    ModelState.AddModelError("Photo","not valid");
                    return View(update);
                }
                if (!update.Photo.ValidateSize(10))
                {
                    update.Positions = await _db.Positions.ToListAsync();
                    ModelState.AddModelError("Photo", "max 10mb");
                    return View(update);
                }
                chef.Image.DeleteFile(_env.WebRootPath,"assets","img");
                chef.Image = await update.Photo.CreateFile(_env.WebRootPath,"assets","img");
            }

            chef.Name= update.Name;
            chef.PositionId= update.PositionId;
            chef.TwitLink= update.TwitLink;
            chef.LinkedLink= update.LinkedLink;
            chef.GoogleLink= update.GoogleLink;
            chef.FaceLink= update.FaceLink;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Chef chef = await _db.Chefs.FirstOrDefaultAsync(x=>x.Id==id);
            if (chef == null) return NotFound();
            
            _db.Chefs.Remove(chef);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
