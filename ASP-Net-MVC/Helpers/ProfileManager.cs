using ASP_Net_MVC.Data;
using ASP_Net_MVC.Models;
using ASP_Net_MVC.Models.Entities;
using ASP_Net_MVC.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASP_Net_MVC.Helpers
{

    public interface IProfileManager
    {
        Task<ProfileResult> CreateAsync(IdentityUser user, UserProfile profile);
        Task<UserProfile> ReadAsync(string userId);
        Task<string> DisplayNameAsync(string userId);
        Task<string> ReadRoleAsync(string userId);

        Task<ProfileViewModel> GetAllProfilesAsync();

        Task<List<RolesModel>> ReadAllRolesAsync();

        Task<IdentityRole> UpdateRoleAsync(string userRole);

        //Task<ProfileResult> EditProfileAsync(IdentityUser user, UserProfile profile);

    }
    public class ProfileManager : IProfileManager
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ProfileManager(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
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
            var profile = new UserProfile()
            {
                Roles = await ReadAllRolesAsync()
            };
            var profileEntity = await _context.Profiles.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userId);
            if (profileEntity != null)
            {
                profile.FirstName = profileEntity.FirstName;
                profile.LastName = profileEntity.LastName;
                profile.Email = profileEntity.User.Email;
                profile.AddressLine = profileEntity.AddressLine;
                profile.PostalCode = profileEntity.PostalCode;
                profile.City = profileEntity.City;
                profile.Country = profileEntity.Country;
                profile.FileName = profileEntity.FileName;
                profile.UserId = profileEntity.UserId;
            };



            return profile;
        }
        //public async Task<UserProfile> ReadAsync(string userId)
        //{
        //    var profile = new UserProfile();
        //    var profileEntity = await _context.Profiles.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userId);
        //    if(profileEntity != null)
        //    {
        //        profile.FirstName = profileEntity.FirstName;
        //        profile.LastName = profileEntity.LastName;
        //        profile.Email = profileEntity.User.Email;
        //        profile.AddressLine = profileEntity.AddressLine;
        //        profile.PostalCode = profileEntity.PostalCode;
        //        profile.City = profileEntity.City;
        //        profile.Country = profileEntity.Country;
        //        profile.FileName = profileEntity.FileName;
        //        profile.UserId = profileEntity.UserId;
        //    };

        //    return profile;
        //}

        public async Task<ProfileViewModel> GetAllProfilesAsync()
        {
            var profileList = new List<UserProfile>();
            var profileEntity = await _context.Profiles.Include(x => x.User).ToListAsync();


            if( profileEntity != null)
            {
                foreach(var profile in profileEntity)
                {
                    profileList.Add(new UserProfile()
                    {
                        Id = profile.UserId,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        Email = profile.User.Email,
                        AddressLine = profile.AddressLine,
                        PostalCode = profile.PostalCode,
                        City = profile.City,
                        Country = profile.Country,
                        FileName = profile.FileName,
                        DisplayName = $"{profile.FirstName} {profile.LastName}",
                        UserId = profile.UserId,
                        
                    });
                }

            }
            var profileView = new ProfileViewModel()
            {
                Profile = profileList,
                Roles = await ReadAllRolesAsync(),
                NewRole = ""
            };

            return profileView;                
        }


        public async Task<string> ReadRoleAsync (string userId)
        {
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(x => x.UserId == userId);
            var role = await _roleManager.FindByIdAsync(userRole.RoleId);
            return role.Name;
        }

        public async Task<List<RolesModel>> ReadAllRolesAsync() 
        {
            var userRoles = await _context.Roles.ToListAsync();
            var userList = new List<RolesModel>();
            foreach(var role in userRoles)
            {
                userList.Add(new RolesModel()
                {
                   RoleId = role.Id,
                   RoleName = role.Name,
                });
            }


            return userList;
      
        }

     public async Task<IdentityRole> UpdateRoleAsync(string userRole)
        {
            
            var newUserRole = new IdentityRole()
            {
                Name = userRole,
            };

            return newUserRole;
        }

        public async Task<string> DisplayNameAsync(string userId)
        {
            var result = await ReadAsync(userId);
            return $"{result.FirstName} {result.LastName}";
        }


    }
    public class ProfileResult
    {
        public bool Succeeded { get; set; } = false;
    }
}
