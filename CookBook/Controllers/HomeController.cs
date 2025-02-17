using Microsoft.AspNetCore.Mvc;

namespace CookBook.Controllers
{
    public class HomeController : Controller
    {
        // GET: 
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}