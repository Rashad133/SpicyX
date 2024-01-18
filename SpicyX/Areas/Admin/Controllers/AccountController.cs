using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpicyX.Areas.Admin.ViewModels.Account;
using SpicyX.Models;
using SpicyX.Utilities.Enums;

namespace SpicyX.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if(!ModelState.IsValid) return View(login);
            AppUser user = await _userManager.FindByNameAsync(login.UsernameOrEmail);
            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(login.UsernameOrEmail);
                if(user == null)
                {
                    ModelState.AddModelError(string.Empty,"wrong mail password username");
                    return View(login);
                }
            }

            var result = await _signInManager.PasswordSignInAsync(user,login.Password,false,true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty,"is locket out");
                return View(login);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "wrong mail password username");
                return View(login);
            }

            return RedirectToAction("index","home",new { Area=""});
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home", new { Area = "" });
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if(!ModelState.IsValid) return View(register);

            AppUser user = new AppUser
            {
                Name=register.Name,
                Email=register.Email,
                Surname=register.Surname,
                UserName=register.UserName,
            };

            var result = await _userManager.CreateAsync(user,register.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                }
                return View(register);
            }

            await _userManager.AddToRoleAsync(user,UserRole.Member.ToString());
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction(nameof(Login));
        }
    }
}
