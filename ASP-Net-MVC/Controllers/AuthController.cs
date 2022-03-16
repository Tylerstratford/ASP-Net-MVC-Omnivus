using ASP_Net_MVC.Data;
using ASP_Net_MVC.Helpers;
using ASP_Net_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Net_MVC.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAddressManager _addressManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IAddressManager addressManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _addressManager = addressManager;
        }



        #region SignUp
        public IActionResult SignUp(string returnUrl = null)
        {
            if(_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }

            var form = new SignUpForm();

            if(returnUrl != null)
            {
                form.ReturnUrl = returnUrl;
            }

            return View(form);
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpForm model)
        {
            if(ModelState.IsValid)
            {
                if(!_userManager.Users.Any())
                {
                    model.RoleName = "admin";
                }

                var user = new AppUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email
                };

                var Response = await _userManager.CreateAsync(user, model.Password);
                if(Response.Succeeded)
                {
                    var address = new AppAddress()
                    {
                        AddressLine = model.AddressLine,
                        PostalCode = model.PostalCode,
                        City = model.City,
                    };

                    await _addressManager.CreateUserAddressAsync(user, address);
                    await _userManager.AddToRoleAsync(user, model.RoleName);

                    if(model.ReturnUrl != null || model.ReturnUrl == "/")
                    {
                        return RedirectToAction("Index", "Home");
                    } else
                    {
                        return LocalRedirect(model.ReturnUrl);
                    }
                }
                foreach(var error in Response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }

        #endregion


        #region SignIn
        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SignIn(SignInForm model)
        {
            return View();
        }

        #endregion

        #region SignOut

        public IActionResult SignOut()
        {
            return View();
        }

        #endregion
    }
}
