using ASP_Net_MVC.Models.Entities;

namespace ASP_Net_MVC.Models.ViewModels
{
    public class ProfileViewModel
    {
        public List<UserProfile> Profile { get; set; }
        public UserProfile profile { get; set; }

        public List<RolesModel> Roles { get; set; }
    }
}
