using ETICARET.Business.Abstract;
using ETICARET.WebUI.Extensions;
using ETICARET.WebUI.Identity;
using ETICARET.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETICARET.WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private ICartService _cartService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ICartService cartService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cartService = cartService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user,model.Password);

            if (result.Succeeded)
            {
                /* 
                 Generate Token
                 Email Service
                 */
                return RedirectToAction("Login","Account");
            }

            ModelState.AddModelError("","Bilinmeyen bir hata oluştu.");

            return View(model);

        }

        public IActionResult Login(string ReturnUrl=null)
        {
            return View(
                new LoginModel()
                {
                    ReturnUrl = ReturnUrl
                }
            );
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            ModelState.Remove("ReturnUrl");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("","Bu email ile daha hesap oluşturulmamıştır.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user,model.Password,true,false);

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "~/"); // ??  null değilse yap nullsa sağdaki durumu yap. model.ReturnUrl == null ? "~/" : model.ReturnUrl
            }

            ModelState.AddModelError("", "Email veya Şifre Yanlış");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            TempData.Put("message", new ResultMessage()
            {
                Title = "Oturumunuz Kapatıldı",
                Message = "Hesabınız güvenli bir şekilde sonlandırıldı.",
                Css="warning"
            });

            return Redirect("~/");
        }
    }
}
