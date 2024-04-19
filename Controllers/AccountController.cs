using BinaAz.Extensions;
using BinaAz.Models.ViewModels;
using BinaAz.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MimeKit;
using BinaAz.Extensions;
using BinaAz.Models;
using BinaAz.Models.ViewModels;
using System.Data;

namespace BinaAz.Controllers
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
		public async Task<IActionResult> Login(AppUserLogin appUserLogin, string returnUrl)
		{
			var user = await _userManager.FindByNameAsync(appUserLogin.Username);
			if (user != null && !await _userManager.IsEmailConfirmedAsync(user))
			{
				var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);
				await _emailService.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");
				return RedirectToAction("Confirmation", "Account");

			}
			var result = await _signInManager.PasswordSignInAsync(appUserLogin.Username, appUserLogin.Password, false, true);
			if (result.Succeeded)
			{
				if (!string.IsNullOrEmpty(returnUrl))
				{
					return Redirect(returnUrl);
				}
				return RedirectToAction("Index", "Home");
			}
			return View(appUserLogin);
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
				AppUser appUser = new AppUser()
				{
					UserName = appUserRegister.Username,
					Email = appUserRegister.Email,
					Name = appUserRegister.Name,
					Surname = appUserRegister.Surname,
				};
				var result = await _userManager.CreateAsync(appUser, appUserRegister.Password);
				if (result.Succeeded)
				{
					var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
					var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = appUser.Id, token = token }, Request.Scheme);
					await _emailService.SendEmailAsync(appUserRegister.Email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");

					await _userManager.AddToRoleAsync(appUser, "User");

					return RedirectToAction("Confirmation", "Account");
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
		[HttpGet]
		public async Task<IActionResult> ConfirmEmail(string userId, string token)
		{
			if (userId == null || token == null)
			{
				return RedirectToAction("Index", "Home");
			}

			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return RedirectToAction("Error", "Home");
			}

			var result = await _userManager.ConfirmEmailAsync(user, token);
			if (result.Succeeded)
			{
				return View("ConfirmEmail");
			}

			return RedirectToAction("Error", "Home");
		}

		[HttpGet]
		public IActionResult Confirmation()
		{
			return View();
		}
		[HttpGet]
		public IActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ForgotPassword(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
				return BadRequest();
			}

			var user = await _userManager.FindByEmailAsync(email);
			if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
			{
				TempData["Message"] = "An email has been sent to your email address with instructions on how to reset your password.";
				return RedirectToAction("Message");
			}

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			var callbackUrl = Url.Action("ResetPassword", "Account", new { email = email, token = token }, Request.Scheme);
			await _emailService.SendEmailAsync(email, "Reset Password", $"Please reset your password by <a href='{callbackUrl}'>clicking here</a>.");

			TempData["Message"] = "An email has been sent to your email address with instructions on how to reset your password.";
			return RedirectToAction("Message");
		}

		[HttpGet]
		public IActionResult ResetPassword(string token, string email)
		{
			if (token == null || email == null)
			{
				return RedirectToAction("Error");
			}

			var model = new ResetPasswordViewModel { Token = token, Email = email };
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user == null)
				{
					return RedirectToAction("Error");
				}
				if (model.Password != model.ConfirmPassword)
				{
					ModelState.AddModelError(string.Empty, "The password and confirmation password do not match.");
					return View(model);
				}
				var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
				if (result.Succeeded)
				{
					TempData["Message"] = "Your password has been reset successfully.";
					return RedirectToAction("Message");
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}

				return View(model);
			}

			return View(model);
		}

		[HttpGet]
		public IActionResult Message()
		{
			ViewBag.Message = TempData["Message"];
			return View();
		}


	}
}