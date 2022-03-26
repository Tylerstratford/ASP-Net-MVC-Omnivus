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

        public AdminController(ApplicationDbContext context, IProfileManager profileManager, RoleManager<IdentityRole> identityRole)
        {
            _context = context;
            _profileManager = profileManager;
            _identityRole = identityRole;
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
       public async Task<IActionResult> DeleteRole(ProfileViewModel model)
        {

            return View();
        }

    }
}
