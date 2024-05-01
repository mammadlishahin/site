using Microsoft.AspNetCore.Mvc;

namespace Controllers;
public class ErrorController : Controller
{

    public ActionResult Error(int? statusCode)
    {
        if (statusCode.HasValue && statusCode.Value == 404)
        {
            return View("404");
        }
        return View("Error");
    }

}