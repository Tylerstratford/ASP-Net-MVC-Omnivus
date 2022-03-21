using ASP_Net_MVC.Data;
using ASP_Net_MVC.Models;
using ASP_Net_MVC.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASP_Net_MVC.Helpers
{

    public interface IProfileManager
    {
        Task<ProfileResult> CreateAsync(IdentityUser user, UserProfile profile);
        Task<UserProfile> ReadAsync(string userId);
    }
    public class ProfileManager : IProfileManager
    {
        private readonly ApplicationDbContext _context;

        public ProfileManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ProfileResult> CreateAsync(IdentityUser user, UserProfile profile)
        {

            if(await _context.Users.AnyAsync(x => x.Id == user.Id))
            {
                var profileEntity = new ProfileEntity
                {
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    AddressLine = profile.AddressLine,
                    City = profile.City,
                    PostalCode = profile.PostalCode,
                    Country = profile.Country,
                    UserId = user.Id,
                };

                _context.Profiles.Add(profileEntity);
                await _context.SaveChangesAsync();

                return new ProfileResult { Succeeded = true };
            }
                return new ProfileResult { Succeeded = false };
        }

        public async Task<UserProfile> ReadAsync(string userId)
        {
            var profile = new UserProfile();
            var profileEntity = await _context.Profiles.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userId);
            if(profileEntity != null)
            {
                profile.FirstName = profileEntity.FirstName;
                profile.LastName = profileEntity.LastName;
                profile.Email = profileEntity.User.Email;
                profile.AddressLine = profileEntity.AddressLine;
                profile.PostalCode = profileEntity.PostalCode;
                profile.City = profileEntity.City;
                profile.Country = profileEntity.Country;
            };

            return profile;
        }
    }
    public class ProfileResult
    {
        public bool Succeeded { get; set; } = false;
    }
}
