using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace WebApplicationFitness.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
