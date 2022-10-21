using Microsoft.AspNetCore.Mvc;

namespace SweetNSavory.Controllers
{
    public class HomeController : Controller
    {

      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }
      
    }
}