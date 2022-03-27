using ASP_Net_MVC.Data;
using ASP_Net_MVC.Helpers;
using ASP_Net_MVC.Models;
using ASP_Net_MVC.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_Net_MVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProfileManager _profileManager;
        private readonly RoleManager<IdentityRole> _identityRole;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(ApplicationDbContext context, IProfileManager profileManager, RoleManager<IdentityRole> identityRole, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _profileManager = profileManager;
            _identityRole = identityRole;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {

            var profiles = await _profileManager.GetAllProfilesAsync();
            return View(profiles);
        }



        [HttpPost]
        public async Task<IActionResult> UserList(ProfileViewModel model)
        {
            await _identityRole.CreateAsync(new IdentityRole(model.NewRole));
            return RedirectToAction("UserList");
        }

        [HttpPost]
       public async Task<IActionResult> DeleteRole(ProfileViewModel role)
        {

            if (role != null)
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.RoleModel.RoleName);

                if (role.RoleModel.RoleName == "user" || role.RoleModel.RoleName == "admin" || role.RoleModel.RoleName == "User" || role.RoleModel.RoleName == "Admin")
                {
                    //user and admin cannot be deleted
                    return RedirectToAction("UserList");
                }

                foreach (var user in usersInRole)
                {
                    
                    await _userManager.AddToRoleAsync(user, "user");
                }

                var removeOld = await _identityRole.FindByNameAsync(role.RoleModel.RoleName);

                await _identityRole.DeleteAsync(removeOld);
            }

            return RedirectToAction("UserList");
        }


        public async Task<IActionResult> EditRole(ProfileViewModel role)
        {
            if (role.RoleModel.RoleName != null)
            {
                if (role.RoleModel.RoleName == "admin" || role.RoleModel.RoleName == "user" || role.RoleModel.RoleName == "Admin" || role.RoleModel.RoleName == "User")
                {
                    return RedirectToAction("UserList");
                }

                var rolesEntity = await _identityRole.FindByNameAsync(role.RoleModel.RoleName);
                rolesEntity.Name = role.RoleModel.NewRoleName;

                var rolesEntityUpdate = await _identityRole.UpdateAsync(rolesEntity);
              
            }

            return RedirectToAction("UserList");
        }

        public async Task<IActionResult> DeleteUser(string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("UserList");

        }

    }
}
