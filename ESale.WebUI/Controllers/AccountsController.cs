using ESale.Business.Abstract;
using ESale.WebUI.Identity;
using ESale.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI.Controllers
{

    [AutoValidateAntiforgeryToken]  // Get dışındaki action lar Validate etmek zorunda
    public class AccountsController : Controller
    {

        private UserManager<ApplicationUser> _userManager;   // 
        private SignInManager<ApplicationUser> _signInManager;   //   Login işleminde cookie koymada logout da
        //private IEmailSender _emailSender;
        private ICartService _cartService;
        private IFavoryBoxService _favoryBoxService;

        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager/*, IEmailSender emailSender*/, ICartService cartService, IFavoryBoxService favoryBoxService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            // _emailSender = emailSender;
            _cartService = cartService;
            _favoryBoxService = favoryBoxService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {


            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    FullName = model.FullName,
                    UserName = model.UserName

                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _cartService.CreateCart(user.Id);
                    _favoryBoxService.CreateFavoryBox(user.Id);

                    // generate token oluşturup email ile kullanıcıya atıcaz
                  //  var tokenCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                   /* var callbackUrl = Url.Action("ConfirmEmail", "Accounts", new
                    {
                        userId = user.Id,
                        token = tokenCode
                    });*/


                   // await _emailSender.SendEmailAsync(model.Email, "Hesabınızı onaylayınız", $"Lütfen email hesabınızı onaylamak için linke <a href='http://localhost:53095{callbackUrl}'>TIKLAYINIZ</a>");

                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);

                    }
                }

                return View(model);
            }

            return View(model);
        }



        /*SİLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL*/

        [HttpGet]
        public IActionResult SilLogin()
        {
            return View();


        }

        [HttpPost]
        public IActionResult SilLogin(LoginModel model)
        {
            return Json("create");

        }



        /*SİLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL*/



        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.returnUrl = returnUrl;
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {

            //returnUrl = returnUrl ?? "~/";

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Bu email ile kayıtlı hesap yok");
                    return View(model);
                }

                //if (!await _userManager.IsEmailConfirmedAsync(user))
                //{
                //    ModelState.AddModelError("", "Lütfen email hesabınıza gelen doğrulama email'ını onaylayın!");
                //    return View(model);
                //}


                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);  // false: Tarayıcı kapanınca cookie silinsin dedik 
                if (result.Succeeded)                                                                                             // checbox ile sorabilirsin
                {
                    return Redirect(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
                }
            }

            ModelState.AddModelError("", "Yanlış email veya parola girdiniz");
            ViewBag.returnUrl = returnUrl;
            return View(model);

        }

        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();
            return Redirect("/");
        }


        //public async Task<IActionResult> ConfirmEmail(string userId, string token)
        //{

        //    if (User == null || token == null)
        //    {
        //        TempData["message"] = "Geçersiz doğrulama!";
        //        return View();
        //    }

        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user != null)
        //    {
        //        var result = await _userManager.ConfirmEmailAsync(user, token);

        //        if (result.Succeeded)
        //        {
        //            _cartService.CreateCart(user.Id);   // Cart kaydı oluşturur database e kaydeder
        //            TempData["message"] = "Başarılı! Hesabınız onaylandı.";
        //            return RedirectToAction("Login");
        //        }



        //    }

        //    TempData["message"] = "BAŞARISIZ ONAYLANAMADI!";
        //    return View();



        //}



        [HttpGet]
        public IActionResult ForgotPassword()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return View();  // Hata mesajı verebilirsin
            }

            var tokenCode = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackUrl = Url.Action("ResetPassword", "Accounts", new
            {
                token = tokenCode
            });


            //await _emailSender.SendEmailAsync(email, "Reset Password", $"Lütfen parolanızı yenilemek için linke <a href='http://localhost:53095{callbackUrl}'>TIKLAYINIZ</a>");


            return RedirectToAction("ResetPassword", new { tokenCode});
        }




        public IActionResult ResetPassword(string tokenCode)
        {
            if (tokenCode == null)
            {
                return RedirectToAction("Home", "Index");
            }
            var model = new ResetPasswordModel { Token = tokenCode };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("Home", "Index");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Accounts", "Login");
            }
            return View(model);
        }


        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
