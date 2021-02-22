using InvoiceApp.Infrastructure.Data.Context;
using InvoiceApp.Infrastructure.Identity;
using InvoiceApp.UI.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApp.UI.MVC.Controllers
{
    [Authorize(Roles = UserRole.Admin)]
    public class UserController : Controller
    {
        UserManager<AppUser> _userManager;
        RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        public async Task<IActionResult> Index([FromServices] ApplicationDbContext db)
        {
            var userVms = new List<UserViewModel>();
            var users = db.Users.ToList();
            foreach (var user in users)
            {
                userVms.Add(new UserViewModel()
                {
                    Key = user.Id,
                    Name = user.UserName,
                    Surname = user.Surname,
                    Email = user.Email,
                    IsAdmin = await _userManager.IsInRoleAsync(user, UserRole.Admin)
                });
            }

            return View(userVms);
        }

        public IActionResult Create()
        {
            return View(new CreateUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel createUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var newUser = new AppUser()
                {
                    Email = createUserViewModel.Email,
                    UserName = createUserViewModel.Name,
                    Surname = createUserViewModel.Surname,
                };

                var roleExists = await _roleManager.RoleExistsAsync(UserRole.Admin);

                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(UserRole.Admin));
                }

                var registerResult = await _userManager.CreateAsync(newUser, createUserViewModel.Password);

                if (registerResult.Succeeded)
                {
                    if (createUserViewModel.IsAdmin)
                    {
                        var addToRoleResult = await _userManager.AddToRoleAsync(newUser, UserRole.Admin);

                        if (!addToRoleResult.Succeeded)
                        {
                            foreach(var error in addToRoleResult.Errors)
                            {
                                ModelState.AddModelError(error.Code, error.Description);
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    foreach(var error in registerResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }

            return View(createUserViewModel);
        }

        public async Task<IActionResult> Delete(string key)
        {
            var user = await _userManager.FindByIdAsync(key);

            if (user == null)
                return NotFound();

            return View(new UserViewModel()
            {
                Key = key,
                Name = user.UserName,
                Surname = user.Surname,
                Email = user.Email,
                IsAdmin = await _userManager.IsInRoleAsync(user, UserRole.Admin)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userViewModel.Key);

                if (user == null)
                    return NotFound();

                var deleteResult = await _userManager.DeleteAsync(user);

                if (deleteResult.Succeeded)
                    return RedirectToAction(nameof(Index));

                foreach(var error in deleteResult.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }

            return View(userViewModel);
        }

        public async Task<IActionResult> Update(string key)
        {
            var user = await _userManager.FindByIdAsync(key);

            if (user == null)
                return NotFound();

            return View(new UpdateUserViewModel()
            {
                Key = key,
                Name = user.UserName,
                Surname = user.Surname,
                Email = user.Email,
                IsAdmin = await _userManager.IsInRoleAsync(user, UserRole.Admin)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserViewModel updateUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var appUser = await _userManager.FindByIdAsync(updateUserViewModel.Key);

                if (appUser == null)
                    return NotFound();

                IdentityResult changePasswordResult = null;
                //change pasword, if current password is not null, so we try to change pass
                if (!string.IsNullOrEmpty(updateUserViewModel.CurrentPassword))
                {
                    changePasswordResult = await _userManager.ChangePasswordAsync(appUser, updateUserViewModel.CurrentPassword, updateUserViewModel.NewPassword);
                }

                //password reseting fails
                if(changePasswordResult != null && !changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
                else
                {
                    //User desent reset pass or reseting pass is successful

                    //update user props
                    appUser.UserName = updateUserViewModel.Name;
                    appUser.Surname = updateUserViewModel.Surname;
                    appUser.Email = updateUserViewModel.Email;

                    var updateResult = await _userManager.UpdateAsync(appUser);

                    if (updateResult.Succeeded)
                    {
                        //updating role
                        bool currentlyIsAdmin = await _userManager.IsInRoleAsync(appUser, UserRole.Admin);
                        IdentityResult addToRoleResult = null;

                        if (updateUserViewModel.IsAdmin)
                        {
                            if (!currentlyIsAdmin)
                            {
                                addToRoleResult = await _userManager.AddToRoleAsync(appUser, UserRole.Admin);
                            }
                        }
                        else
                        {
                            if (currentlyIsAdmin)
                            {
                                addToRoleResult = await _userManager.RemoveFromRoleAsync(appUser, UserRole.Admin);
                            }
                        }

                        if (addToRoleResult != null)
                        {
                            if (!addToRoleResult.Succeeded)
                            {
                                foreach (var error in addToRoleResult.Errors)
                                {
                                    ModelState.AddModelError(error.Code, error.Description);
                                }
                            }
                            else
                            {
                                return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }

            return View(updateUserViewModel);
        }
    }
}
