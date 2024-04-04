using BinaAz.Models.ViewModels;
using BinaAz.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BinaAz.Extensions;
using BinaAz.Models.ViewModels;
using System.Data;

namespace OnlineFashionStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AppUserLogin appUserLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(appUserLogin.Username, appUserLogin.Password, false, true);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(appUserLogin.Username);
                return RedirectToAction("Index", "Home");
                if (user.EmailConfirmed == true)
                {
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(AppUserRegister appUserRegister)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int code;
                code = random.Next(100000, 1000000);
                AppUser appUser = new AppUser()
                {
                    UserName = appUserRegister.Username,
                    Email = appUserRegister.Email,
                    Name = appUserRegister.Name,
                    Surname = appUserRegister.Surname,
                    //ConfirmCode = code
                };
                var result = await _userManager.CreateAsync(appUser, appUserRegister.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, "User");
                    await _emailService.SendEmailAsync(appUser.Email, "Confirm Email", $"{code}");

                    TempData["mail"] = appUserRegister.Email;
                    return RedirectToAction("Login", "Account");
                    return RedirectToAction("ConfirmMail", "ConfirmMail");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View(appUserRegister);
        }
    }
}