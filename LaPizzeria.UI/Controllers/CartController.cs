using Microsoft.AspNetCore.Mvc;

namespace LaPizzeria.UI.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
