using ClothingShop.DTO;
using ClothingShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClothingShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Role()
        {
            return View(_roleManager.Roles.ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Role role)
        {
            AppRole appRole = new AppRole()
            {
                Name = role.Name
            };
            var result = await _roleManager.CreateAsync(appRole);
            return RedirectToAction("Role");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var result = await _roleManager.FindByIdAsync(id);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AppRole role)
        {
            await _roleManager.UpdateAsync(role);
            return RedirectToAction("Role");
        }
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(result);
            return RedirectToAction("Role");
        }
        [HttpGet]
        public async Task<IActionResult> AssignRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            TempData["UserId"] = user.Id;
            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);
            var list = new List<AssignRole>();
            foreach (var item in roles)
            {
                var model = new AssignRole()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Status = userRoles.Contains(item.Name)
                };
                list.Add(model);
            }

            return View(list);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(List<AssignRole> list)
        {
            var user = await _userManager.FindByIdAsync(TempData["UserID"].ToString());
            foreach (var item in list)
            {
                if (item.Status)
                {
                    await _userManager.AddToRoleAsync(user, item.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.Name);
                }
            }

            return RedirectToAction("User","User");
        }
    }
}
