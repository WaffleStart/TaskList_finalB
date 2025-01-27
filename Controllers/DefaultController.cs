using Microsoft.AspNetCore.Mvc;

namespace TaskList.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
