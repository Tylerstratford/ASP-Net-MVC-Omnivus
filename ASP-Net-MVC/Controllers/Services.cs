using Microsoft.AspNetCore.Mvc;

namespace ASP_Net_MVC.Controllers
{
    public class Services : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
