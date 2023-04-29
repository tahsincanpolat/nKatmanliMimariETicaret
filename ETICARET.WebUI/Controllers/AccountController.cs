using ETICARET.Business.Abstract;
using ETICARET.WebUI.EmailService;
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
                // generate token
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = code
                });

                // send email

                string siteurl = "https://localhost:5174";
                string activeUrl = $"{siteurl}{callbackUrl}";

                string body = $"Hesabınızı onaylayınız.<br><br> Lütfen email hesabınızı onaylamak için linke <a href='{activeUrl}' target='_blank'> tıklayız</a>...";

                MailHelper.SendEmail(body,model.Email,"ETICARET HESAP AKTİFLEŞTİRME");
                return RedirectToAction("Login","Account");
            }

            ModelState.AddModelError("","Bilinmeyen bir hata oluştu.");

            return View(model);

        }

        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if(userId == null || token == null)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Geçersiz Token",
                    Message = "Hesap onay bilgileri yanlış",
                    Css = "danger"
                });

                return Redirect("~/");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if(user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                {
                    _cartService.InitializeCart(userId);

                    TempData.Put("message", new ResultMessage()
                    {
                        Title = "Hesap Onayı",
                        Message = "Hesap onaylanmıştır",
                        Css = "danger"
                    });

                    return RedirectToAction("Login","Account");
                }
            }

            TempData.Put("message", new ResultMessage()
            {
                Title = "Hesap Onayı",
                Message = "Hesap onaylanmadı",
                Css = "danger"
            });

            return Redirect("~/");
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

        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Forgot Password",
                    Message = "Bilgileriniz Hatalı",
                    Css = "danger"
                });

                return View();
            }

            var user = await _userManager.FindByEmailAsync(Email);

            if(user == null)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Forgot Password",
                    Message = "Bu Email adresi ile bir kullanıcı bulunamadı",
                    Css = "danger"
                });

                return View();
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account", new
            {
                token = code
            });

            // send email

            string siteurl = "https://localhost:5174";
            string activeUrl = $"{siteurl}{callbackUrl}";

            string body = $"Parolanızı yenilemek için <a href='{activeUrl}' target='_blank'> tıklayız</a>...";

            MailHelper.SendEmail(body, Email, "ETICARET Şifre Yenileme");

            return RedirectToAction("Login");
        }

        public IActionResult ResetPassword(string token)
        {
            if(token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new ResetPassword() { Token = token };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user == null)
            {
                return RedirectToAction("Index","Home");
            }

            var result = await _userManager.ResetPasswordAsync(user,model.Token,model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login","Account");
            }

            return View("~/");
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
