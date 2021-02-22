using InvoiceApp.Infrastructure.Identity;
using InvoiceApp.UI.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApp.UI.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl = "")
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

                if (user != null)
                {
                    var signInResult = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);

                    if (signInResult.Succeeded)
                    {
                        if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                            return RedirectToAction("Index", "Home");

                        return LocalRedirect(loginViewModel.ReturnUrl);
                    }
                }

                ModelState.AddModelError("", "Email or password is incorrect");
            }

            return View(loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut(string returnUrl = "")
        {
            await _signInManager.SignOutAsync();
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "/home/Index";

            return LocalRedirect(returnUrl);
        }
    }
}
