using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieMVC.Admin.Models;
using MovieWebApi.Domain.POCO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieMVC.Admin.Models.Account;

namespace MovieMVC.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        #region Fields
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        #endregion

        #region Ctor
        public AccountController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion

        #region SignIn & SignOut
        [AllowAnonymous]
        public IActionResult Login() => View(new LoginViewModel());


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var password = await _userManager.CheckPasswordAsync(user, model.Password);
                var isAdmin = await _userManager.IsInRoleAsync(user, "Admin"); 

                if (password && isAdmin)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("LstAccounts");
                    }

                    TempData["Error"] = "Invalid Login! Try Again...";
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        #endregion


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> LstAccounts()
        {
            var users = await _userManager.Users.ToListAsync();
            var accountRolesViewModel = new List<AccountRolesViewModel>();

            foreach (var user in users)
            {
                var thisViewModel = new AccountRolesViewModel();
                thisViewModel.Id = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.FullName = user.FullName;

                thisViewModel.Roles = await GetUserRoles(user);
                accountRolesViewModel.Add(thisViewModel);
            }
            return View(accountRolesViewModel);
        }

        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (!isAdmin)
            {
                await _userManager.DeleteAsync(user);
                return RedirectToAction("LstAccounts");
            }
            else
            {
                TempData["Error"] = "U can not delete the Admin";
                return RedirectToAction("LstAccounts");
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            ViewBag.UserName = user.UserName;
            var model = new List<ManageAccountRolesViewModel>();

            var roles = await _roleManager.Roles.ToListAsync();
            foreach (var role in roles)
            {
                var accountRolesViewModel = new ManageAccountRolesViewModel
                {                     
                    Id = role.Id,
                    RoleName = role.Name
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    accountRolesViewModel.Selected = true;
                }
                else
                {
                    accountRolesViewModel.Selected = false;
                }
                model.Add(accountRolesViewModel);
            }
            return View(model);
        }

        //Role Management
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Details(List<ManageAccountRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            return RedirectToAction("LstAccounts");
        }

    }
}
