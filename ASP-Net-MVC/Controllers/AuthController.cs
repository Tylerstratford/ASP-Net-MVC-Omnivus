using ASP_Net_MVC.Data;
using ASP_Net_MVC.Helpers;
using ASP_Net_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_Net_MVC.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IProfileManager _profileManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IProfileManager profileManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _profileManager = profileManager;
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

                if (!await _roleManager.Roles.AnyAsync())
                {
                    await _roleManager.CreateAsync(new IdentityRole("admin"));
                    await _roleManager.CreateAsync(new IdentityRole("user"));
                }

                if (!await _userManager.Users.AnyAsync())
                {
                    model.RoleName = "admin";
                }

                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == model.Email);

                if (user == null)
                {
                    user = new IdentityUser()
                    {
                        UserName = model.Email,
                        Email = model.Email
                    };

                   
                    var userResult = await _userManager.CreateAsync(user, model.Password);
                    if(userResult.Succeeded)

                    {
                        await _userManager.AddToRoleAsync(user, model.RoleName);

                        var profile = new UserProfile
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            AddressLine = model.AddressLine,
                            PostalCode = model.PostalCode,
                            City = model.City,
                            Country = model.Country,
                        };

                        var profileResult = await _profileManager.CreateAsync(user, profile);
                        if (profileResult.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);

                            if(model.ReturnUrl == null || model.ReturnUrl == "/")
                                return RedirectToAction("Index", "Home");
                            else 
                                return LocalRedirect(model.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "SomethingWentWrong");
                           
                        }
                    }
                }

                else
                {
                    return RedirectToAction("Index", "ErrorEmail");

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
