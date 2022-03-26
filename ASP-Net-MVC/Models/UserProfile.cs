using ASP_Net_MVC.Models.Entities;
using ASP_Net_MVC.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace ASP_Net_MVC.Models
{
    public class UserProfile
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AddressLine { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;  
        public string FileName { get; set; }
        public string FriendlyFileName { get; set; }
        public IFormFile? File { get; set; }

        public string DisplayName { get; set;}


        public List<RolesModel> Roles { get; set; }


        public RolesModel RolesModel { get; set; }
        public IdentityRole Roll { get; set; }

        public string Name { get; set; } = string.Empty;
        public string RoleName { get; set; }

    }
}
