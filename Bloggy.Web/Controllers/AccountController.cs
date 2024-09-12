using Bloggy.Web.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggy.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModels registerViewModels)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerViewModels.Username,
                Email = registerViewModels.Email,
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerViewModels.Password);
            if (identityResult.Succeeded)
            {
                //Assign user role
                var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");
                if (roleIdentityResult.Succeeded)
                {
                    //Show success notification
                    return RedirectToAction("Register");
                }
            }
            //Show error notification
            return RedirectToAction("Register");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(model); 
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var signInResult = await signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);
            
            if (signInResult != null && signInResult.Succeeded) 
            {
                if(!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                return Redirect(loginViewModel.ReturnUrl);
            }
            //Show error
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
