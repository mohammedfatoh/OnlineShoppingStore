using Microsoft.AspNetCore.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
