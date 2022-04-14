using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieMVC.General.Models.Account;
using MovieWebApi.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMVC.General.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login() => View(new LoginViewModel());

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel Login)
        {
            if (!ModelState.IsValid) return View(Login);
            var user = await _userManager.FindByEmailAsync(Login.EmailAddress);

            if (user != null)
            {
                var password = await _userManager.CheckPasswordAsync(user, Login.Password);
                
                if (password)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, Login.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movie");
                    }

                    TempData["Error"] = "Please, Try Again!";
                    return View(Login);
                }
            }
            TempData["Error"] = "Wrong, Try Again";
            return View(Login);
        }

        public IActionResult Register() => View(new RegisterViewModel());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid) return View(register);
            var user = await _userManager.FindByEmailAsync(register.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "the same Account is already Exists";
                return View(register);
            }

            var newUser = new ApplicationUser()
            {
                FullName = register.FullName,
                Email = register.EmailAddress,
                UserName = register.FullName
            };

            var result = await _userManager.CreateAsync(newUser, register.EmailAddress);

            if (result.Succeeded)
            {
                return View("RegisterCompleted");              
            }

            return View("Error");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movie");
        }
    }
}
