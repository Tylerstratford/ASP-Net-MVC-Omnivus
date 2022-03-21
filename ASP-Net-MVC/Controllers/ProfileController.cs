using ASP_Net_MVC.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Net_MVC.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileManager _profileManager;

        public ProfileController(IProfileManager profileManager)
        {
            _profileManager = profileManager;
        }

        [HttpGet("{id}")]
        [Route("Profile/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            var profile = await _profileManager.ReadAsync(id);
            return View(profile);
        }
    }
}
