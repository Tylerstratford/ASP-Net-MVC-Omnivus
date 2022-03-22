using ASP_Net_MVC.Data;
using ASP_Net_MVC.Helpers;
using ASP_Net_MVC.Models;
using ASP_Net_MVC.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_Net_MVC.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileManager _profileManager;
        private readonly ApplicationDbContext _context;


        public ProfileController(IProfileManager profileManager, ApplicationDbContext context)
        {
            _profileManager = profileManager;
            _context = context;
        }

        [HttpGet("profile/{id}")]
        [Route("Profile/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            var profile = await _profileManager.ReadAsync(id);
            return View(profile);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> EditProfile(string id)
        {
            var profile = await _profileManager.ReadAsync(id); 
            return View(profile);
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> EditProfile(string id, UserProfile model)
        {
            var profile = new UserProfile();
            var profileEntity = await _context.Profiles.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == id);
            var identityUserEntity = await _context.Users.FirstOrDefaultAsync(x => x.Email == profileEntity.User.Email);

            if(profileEntity != null) 
            {
            profileEntity.FirstName = model.FirstName;
                profileEntity.LastName = model.LastName;
                profile.Email = model.Email;
                profileEntity.City = model.City;
                profileEntity.Country = model.Country;
                profileEntity.AddressLine = model.AddressLine;
                profileEntity.PostalCode = model.PostalCode;
            }

            if(identityUserEntity != null)
            {
                identityUserEntity.Email = model.Email;
                //identityUserEntity.UserName = identityUserEntity.Email;  ****Username remains same on creation and is needed for log in
            }

            _context.Entry(identityUserEntity).State = EntityState.Modified;
            _context.Entry(profileEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Profile", new {Id = id});
            //return View(model);

        }

   

    }
}
