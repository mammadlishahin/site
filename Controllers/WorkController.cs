using Namespace_Iyzipay;
using Microsoft.AspNetCore.Mvc;
using Models;
using Iyzipay.Model;
using EfCore;
using Microsoft.AspNetCore.Authorization;
namespace Controllers;

public class WorkController : Controller
{
    private DB dB;
    private DateTime SuAnkiTurkiyeZamani;
    public WorkController()
    {
        DateTime turkiyeSaati = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));
        SuAnkiTurkiyeZamani = new DateTime(turkiyeSaati.Year, turkiyeSaati.Month, turkiyeSaati.Day, turkiyeSaati.Hour, turkiyeSaati.Minute, 0);
        dB = new DB();
    }


    public IActionResult NaciyeHakkinda()
    {
        return View();
    }
    [Authorize]
    [Authorize(Roles = "User")]
    public IActionResult Profil()
    {
        return View();
    }
    [Authorize]
    [Authorize(Roles = "User")]
    public IActionResult Terapiler()
    {
        
        if(HttpContext!=null){if(HttpContext.User!=null){if(HttpContext.User.Identity!=null){if(HttpContext.User.Identity.Name!=null){
            User user = dB.GetUser(HttpContext.User.Identity.Name);
            if(user!=null){
                List<Models.Terapiler> ButunTerapiler = dB.ResultTherapy(user);
                if(ButunTerapiler!=null){
                    ViewBag.Terapiler = ButunTerapiler.OrderBy(terapi => terapi.TherapyStatus).ThenBy(terapi => terapi.StartDateTime).ToList();
                }
                else{
                    ViewBag.Terapiler = new List<Models.Terapiler>();
                    return View(new PaymentCardAndTerapiler());
                }
            }
            else
            {
                return View(new PaymentCardAndTerapiler());
            }
        }}}}
        ViewBag.Terapiler = new List<Models.Terapiler>();
        return View(new PaymentCardAndTerapiler());



    }


    [HttpPost]
    public async Task<IActionResult> SoruSor(Soru soru)
    {
        ModelState.Remove("User");
        if (soru != null && ModelState.IsValid)
        {
            soru.Date = SuAnkiTurkiyeZamani;
            if (soru.Ad!=null&& soru.Email!=null&&soru.Mesaj!=null)
            dB.AddSoru(soru);
        }
        else
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors)) { Console.WriteLine(error.ErrorMessage); }
        }
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    [Authorize(Roles = "User")]
    public IActionResult Bank_Cart(PaymentCardAndTerapiler CardAndTerapi)
    {
        if (CardAndTerapi != null) { if (CardAndTerapi.Terapi != null) { if (CardAndTerapi.Terapi.AppName != null && CardAndTerapi.Terapi.Type != null) { if(dB.MusaitTerapiVakiti(CardAndTerapi.Terapi.StartDateTime)){
            return View(CardAndTerapi);
        }}}}
        return RedirectToAction("Terapiler", "Work", CardAndTerapi);
    }

    [Authorize]
    [Authorize(Roles = "User")]
    [HttpPost]
    public IActionResult SuccessPeyment(PaymentCardAndTerapiler CardAndTerapi)
    {
        bool kapi=false;
        User? user=null;
        if(HttpContext!=null){if(HttpContext.User!=null){if(HttpContext.User.Identity!=null){if(HttpContext.User.Identity.Name!=null){
            user = dB.GetUser(HttpContext.User.Identity.Name);
        }}}}
       
        if (user == null)
        {
            return View("Bank_Cart", CardAndTerapi);
        }
        if(CardAndTerapi!=null){if(CardAndTerapi.Card!=null){if(CardAndTerapi.Terapi!=null){if(CardAndTerapi.Terapi.Type!=null){if(CardAndTerapi.Terapi.AppName!=null){
           kapi=true;
        }}}}}
        if (kapi) //if kapi
        {
             My_Iyzipay pay = new My_Iyzipay();

            string email = user.Email; // Email alani kullanicinin fatura adresini bildirir
            CardAndTerapi.Terapi.UserId = user.Id;
            CardAndTerapi.Terapi.CreationDateTime = SuAnkiTurkiyeZamani;
            Payment payment = pay.Ode(CardAndTerapi.Card, email);
            Console.WriteLine("Payment Status: " + payment.Status);
            if (payment.Status == "success")
            {
                dB.AddTherapy(CardAndTerapi.Terapi);
                TempData["ISsuccess"] = 123;
                return RedirectToAction("Terapiler", "Work");
            }
            else 
            {
                TempData["ISsuccess"] = 123;
                Console.WriteLine("ErrorMessage: " + payment.ErrorMessage);
                return View("Bank_Cart", CardAndTerapi);
            }
        }//if kapi
        return View("Bank_Cart", CardAndTerapi);
        
    }// SuccessPeyment()


}
