using ASP_Net_MVC.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace ASP_Net_MVC.Models
{
    public class UserProfile
    {

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AddressLine { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public string FileName { get; set; }
        public string FriendlyFileName { get; set; }
        public IFormFile? File { get; set; }
    }
}
