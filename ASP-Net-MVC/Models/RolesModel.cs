using Microsoft.AspNetCore.Identity;

namespace ASP_Net_MVC.Models
{
    public class RolesModel
    {

        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public string Name { get; set; }

        public IdentityUser Role { get; set; }

        public IdentityRole Roles { get; set; }
    }
}
