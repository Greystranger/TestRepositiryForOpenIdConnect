using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWithIdentity.Models;
using AspNetCoreWithIdentity.Services;
using AspNetCoreWithIdentity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWithIdentity.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();

            return View(roles);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var role = new IdentityRole(name);
                var createRoleResult = await _roleManager.CreateAsync(role);

                if (createRoleResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ErrorHandlerService.AddErrorsToModelState(ModelState, createRoleResult);
            }

            return View(name);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                var model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };

                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var addedRolesPerUser = roles.Except(userRoles);
                var removedRolesPerUser = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRolesPerUser);
                await _userManager.RemoveFromRolesAsync(user, removedRolesPerUser);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult UserList()
        {
            var users = _userManager.Users.ToList();

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var deleteResult = await _roleManager.DeleteAsync(role);
                if (!deleteResult.Succeeded)
                {
                    ErrorHandlerService.AddErrorsToModelState(ModelState, deleteResult);
                }
            }
            else
            {
                ModelState.AddModelError("", "Cannot find and delete role with specified id");
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}