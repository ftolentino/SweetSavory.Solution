using Microsoft.AspNetCore.Mvc;

namespace SweetSavory.Controllers
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
