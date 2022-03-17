using ASP_Net_MVC.Data;

namespace ASP_Net_MVC.Models.ViewModels
{
    public class ProfileViewModel
    {
        public AppUserAddress Address { get; set; }
        public AppUser User { get; set; }
    }
}
