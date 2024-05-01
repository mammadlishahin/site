using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EfCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Models;
using SMTP;




public class AuthController : Controller
{
    private DB dB;
    public AuthController()
    {
        dB = new DB();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(User login)
    {
        sbyte StatusCode = 0;
        // Eğer 'Terapiler' adlı proportisi boş gelirse ModelState.IsValid hatasını Önler.
        ModelState.Remove("Terapiler");
        ModelState.Remove("EmaiVerification");
        ModelState.Remove("VerificationCode");

        if (login.Id == 2 && login != null)// Giriş Yap. 
        {
            ViewBag.PostType = login.Id;
            ModelState.Remove("Email"); // Eğer kullanıcı kayıt değilde giriş yapmışsa Email alanının boş olmasını görmezden gel
            if (ModelState.IsValid)
            {
                login.Password = Password_To_SHA256(login.Password);
                User user = dB.SingIn(login);
                StatusCode = user == null ? (sbyte)5 : (sbyte)2;
                if (StatusCode == 2)
                {
                    if (user != null)
                        await Yetkilendirme(user.UserName, user.UserType);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.StatusCode = StatusCode;
                    Console.WriteLine(ViewBag.StatusCode);
                    return View();
                }
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors)) { Console.WriteLine(error.ErrorMessage); }
                return View();
            }
        }// Giriş Yap. end
        else if (login.Id == 1 && login != null)// Kayıt Ol.
        {
            ViewBag.PostType = login.Id;
            if (ModelState.IsValid)
            {
                login.Password = Password_To_SHA256(login.Password);
                StatusCode = await dB.AuthUser(login);
                ViewBag.StatusCode = StatusCode;
                if (StatusCode == 1)
                {
                    TempData["MailAdresiniDogrula"] = "success";
                    return RedirectToAction("LoginVerification", "Auth");
                }
                else
                {
                    return View();
                }

            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors)) { Console.WriteLine(error.ErrorMessage); }
                return View();
            }
        }// Kayıt Ol. end
        else
        {
            return View();
        }
    }// Login()

    [HttpPost]
    public async Task<IActionResult> LogOut()
    {
        HttpContext.Session.Clear();
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Auth");
    }

    public async Task<IActionResult> Auth(string Change)
    {

        User user = dB.EmailVerification(Change);
        if (user != null)
        {
            await Yetkilendirme(user.UserName);
            TempData["Verification"] = "success";
            return RedirectToAction("LoginVerification", "Auth");
        }
        TempData["Verification"] = "failure";
        return RedirectToAction("LoginVerification", "Auth");
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(string email)
    {

        User? user = dB.GetUserEmail(email);
        if (user != null && !string.IsNullOrEmpty(email))
        {
            string urlCod = PasswordGenerator.GeneratePassword(100, 150);
            user.VerificationCode = urlCod;
            dB.UpdateUser(user);
            await Mail.ResetPassword(email, urlCod);
            TempData["ResetPassword"] = "send";
            return RedirectToAction("LoginVerification", "Auth");
        }
        TempData["getMail"] = "failure";
        return RedirectToAction("Login", "Auth");
    }

    public IActionResult PasswordResetVerification(string urlCode)
    {
        User? user = null;
        if (!string.IsNullOrEmpty(urlCode))
        {
            user = dB.PasswordResetVerification(urlCode);
        }
        if (user != null)
        {
            return View(user);
        }

        TempData["finishurl"] = "suresibitmis";
        return RedirectToAction("LoginVerification", "Auth");
        
    }

    public IActionResult UpdatePassword(string password,int id){

        if (!string.IsNullOrEmpty(password)){
            if(password.Length>7&&password.Length<=100){
                password=Password_To_SHA256(password);
                dB.UpdatePassword(password,id);
                TempData["resetUrlCode"] = "success";
                return RedirectToAction("LoginVerification", "Auth");
            }
        }
        TempData["resetUrlCode"] = "failure";
        return RedirectToAction("LoginVerification", "Auth");
    }
    public IActionResult LoginVerification()
    {
        return View();
    }
    [NonAction]
    private string Password_To_SHA256(string _password)
    {
        string HashPassword = "";
        using (SHA256 sha256 = SHA256.Create())
        {
            // Metni byte dizisine dönüştürün
            byte[] inputBytes = Encoding.UTF8.GetBytes(_password);

            // Hash değerini hesaplayın
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            // Hash değerini hex formata dönüştürün
            HashPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        } // using SHA256
        return HashPassword;
    }

    [NonAction]
    private async Task Yetkilendirme(string userName, sbyte userType = 1)
    {
        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role,userType==2?"Admin":"User"),
                };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        // Kullanıcıyı yetkilendir ve kimlik doğrulama çerezini oluştur
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

    }

}

