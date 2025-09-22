using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login([FromQuery(Name = "ReturnUrl")] string ReturnUrl)
        {

            return View(new LoginModel { ReturnUrl=ReturnUrl});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByNameAsync(model.Name);

                if (user is not null)
                {
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
                        return Redirect(model?.ReturnUrl ?? "/");
                }
                ModelState.AddModelError("Error", "Invalid pasword or username");
            }
            return View();
        }
        public async Task<IActionResult> Logout([FromQuery(Name ="ReturnUrl")] string ReturnUrl="/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(ReturnUrl);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Register(RegisterDto model)
        {
            var user = new IdentityUser()
            {
                UserName = model.UserName,
                Email = model.Email
            };
               var result= await _userManager
                .CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
                var roles = await _userManager.AddToRoleAsync(user, "User");

                if(roles.Succeeded)
                return RedirectToAction("Login",new {ReturnUrl="/"});
            }
            else
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View();

        }
        public IActionResult AccessDenied([FromQuery(Name ="ReturnUrl")] string returnUrl)
        {
            return View();
        }
   


    }
}
