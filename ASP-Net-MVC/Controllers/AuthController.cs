using ASP_Net_MVC.Data;
using ASP_Net_MVC.Helpers;
using ASP_Net_MVC.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Route("Signup")]
        [HttpGet]
        public IActionResult SignUp(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }



            var form = new SignUpForm();

            if (returnUrl != null)
            {
                form.ReturnUrl = returnUrl;
            }

            return View(form);
        }

        [Route("Signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpForm model)
        {
            if (ModelState.IsValid)
            {

                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("admin"));
                    await _roleManager.CreateAsync(new IdentityRole("user"));
                }

                if (!_userManager.Users.Any())
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

                var response = await _userManager.CreateAsync(user, model.Password);

                if (response.Succeeded)
                {
                    var address = new AppAddress()
                    {
                        AddressLine = model.AddressLine,
                        PostalCode = model.PostalCode,
                        City = model.City,
                    };

                    await _addressManager.CreateUserAddressAsync(user, address);
                    await _userManager.AddToRoleAsync(user, model.RoleName);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (model.ReturnUrl != null || model.ReturnUrl == "/")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return LocalRedirect(model.ReturnUrl);
                    }
                }
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }

        #endregion


        #region SignIn
        public IActionResult SignIn(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            var model = new SignInForm();

            if (returnUrl != null)
                model.ReturnUrl = returnUrl;

            ViewData["Error"] = "";
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInForm model)
        {
            if (ModelState.IsValid)
            {
                var response = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, false);
                if (response.Succeeded)
                    if (model.ReturnUrl == null || model.ReturnUrl == "/")
                        return RedirectToAction("Index", "Home");
                    else
                        return LocalRedirect(model.ReturnUrl);


            }

            ModelState.AddModelError(String.Empty, "Incorrect email or password");
            ViewData["Error"] = "Incorrect email or password";

            model.Password = "";

            return View(model);
        }

        #endregion

        #region SignOut

        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            if (_signInManager.IsSignedIn(User))
            {
                await _signInManager.SignOutAsync();

            }
            return RedirectToAction("Index", "Home");

        }

        #endregion
    }
}
