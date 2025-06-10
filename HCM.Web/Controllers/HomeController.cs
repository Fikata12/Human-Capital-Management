using Microsoft.AspNetCore.Mvc;

namespace HCM.Web.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
