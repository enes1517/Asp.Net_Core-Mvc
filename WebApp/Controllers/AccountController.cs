using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using Repositories;
using StoreApp.mail;
using System.Threading.Tasks;

namespace StoreApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RepositoryContext _context;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RepositoryContext context,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _emailSender = emailSender;
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
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // Şifremi Unuttum Formu (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Veritabanından kullanıcıyı e-posta ile bul
            var person = await _context.Persons
                .FirstOrDefaultAsync(u => u.Email == model.Email);
            if (person == null)
            {
                // Güvenlik için genel bir mesaj
                return View("ForgotPasswordConfirmation");
            }

            // Şifre sıfırlama token'ı oluştur
            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            person.ResetToken = token;
            person.ResetTokenExpiration = DateTime.UtcNow.AddHours(24); // 24 saat geçerli
            _context.Update(person);
            await _context.SaveChangesAsync();

            // Şifre sıfırlama bağlantısı
            var callbackUrl = Url.Action("ResetPassword", "Account",
                new { userId = person.Id, token = token }, protocol: Request.Scheme);

            // Kullanıcı mail gönderilmesini istediyse
            if (model.SendResetEmail)
            {
                // Veritabanından çekilen e-posta adresine mail gönder
                await _emailSender.SendEmailAsync(
                    person.Email,
                    "Şifre Sıfırlama",
                    $"Şifrenizi sıfırlamak için <a href='{callbackUrl}'>buraya tıklayın</a>.");
            }
            else
            {
                // Mail gönderilmesini istemiyorsa, bağlantıyı view'da göster
                ViewBag.ResetLink = callbackUrl;
            }

            return View("ForgotPasswordConfirmation");
        }

        // Şifre Sıfırlama Sayfası (GET)
        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return BadRequest("Geçersiz sıfırlama bağlantısı.");
            }

            var model = new ResetPasswordViewModel { UserId = userId, Token = token };
            return View(model);
        }

        // Şifre Sıfırlama (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.Persons
                .FirstOrDefaultAsync(u => u.Id == int.Parse(model.UserId) && u.ResetToken == model.Token && u.ResetTokenExpiration > DateTime.UtcNow);

            if (user == null)
            {
                return View("ResetPasswordConfirmation");
            }

            // Şifreyi güncelle (örneğin, BCrypt ile)
            user.ResetToken = null; // Token'ı sıfırla
            user.ResetTokenExpiration = null;
            _context.Update(user);
            await _context.SaveChangesAsync();

            return View("ResetPasswordConfirmation");
        }



    }
}
