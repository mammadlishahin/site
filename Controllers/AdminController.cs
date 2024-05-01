using EfCore;

using Microsoft.AspNetCore.SignalR;
using Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SMTP;

namespace Controllers;
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private DB dB;
    private DateTime SuAnkiTurkiyeZamani;
    public AdminController()
    {
        DateTime turkiyeSaati = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));
        SuAnkiTurkiyeZamani = new DateTime(turkiyeSaati.Year, turkiyeSaati.Month, turkiyeSaati.Day, turkiyeSaati.Hour, turkiyeSaati.Minute, 0);

        dB = new DB();
    }
    public IActionResult Panel()
    {
        return View();
    }
    public IActionResult Seanslar()
    {
        List<UserAndTherapy> ButunTerapiler = dB.AllTherapy();
        ButunTerapiler = ButunTerapiler.OrderBy(terapi => terapi.Therapy.TherapyStatus).ThenBy(terapi => terapi.Therapy.StartDateTime).ToList();
        return View(ButunTerapiler);
    }

    public IActionResult SorulanSorular()
    {
        List<Soru> sorular = dB.AllSoru();
        sorular = sorular.OrderBy(soru => soru.Okundu).ThenBy(soru => soru.Date).ToList();
        return View(sorular);
    }

    public IActionResult SoruEnd(int Id)
    {
        dB.SoruFinsh(id: Id);
        return RedirectToAction("SorulanSorular");
    }

    [HttpPost]
    public IActionResult Yanit(int Id,string email,string yanit)
    {
        Mail.SoruyaYanit(email:email, message:yanit);
        dB.SoruFinsh(id: Id);
        return RedirectToAction("SorulanSorular");
    }

    public IActionResult Kullanici(string user)
    {
        User userObject = dB.GetUser(user);

        return View(userObject);
    }

    [HttpPost]
    public IActionResult SendUrl(int id, string url)
    {
        dB.AddTherapyUrl(id, url);
        return RedirectToAction("Seanslar");
    }
    public IActionResult EndTherapy(int id)
    {
        dB.FinishTherapy(id);
        return RedirectToAction("Seanslar");
    }

    [HttpPost]
    public IActionResult ChangeTherapyDataTime(int id, DateTime zaman)
    {
        bool musaitmi = dB.MusaitTerapiVakiti(zaman);
        if (dB.MusaitTerapiVakiti(zaman))
        {
            TempData["ChangeDataTime"] = "musait";
            dB.UpdateTherapyDateTime(id, zaman);
        }
        else
        {
            TempData["ChangeDataTime"] = "musaitdegil";
        }

        return RedirectToAction("Seanslar");
    }


}
