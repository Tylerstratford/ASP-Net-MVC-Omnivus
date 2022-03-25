using ASP_Net_MVC.Data;
using ASP_Net_MVC.Helpers;
using ASP_Net_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_Net_MVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProfileManager _profileManager;

        public AdminController(ApplicationDbContext context, IProfileManager profileManager)
        {
            _context = context;
            _profileManager = profileManager;
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

        
    }
}
