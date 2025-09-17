using Microsoft.AspNetCore.Mvc;

namespace ParkingServiceApi.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
