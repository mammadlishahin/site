using Microsoft.AspNetCore.Mvc;
namespace Controllers;

public class HomeController : Controller
{

    public async Task<IActionResult> Index()
    {
        List<string> HastalikaDlari = new List<string>(){
            "Depresyon",
            "Anksiyete Bozuklukları",
            "Bipolar Bozukluk",
            "Obsesif Kompulsif Bozukluk (OKB)",
        };

        return View(HastalikaDlari);
    }
}
