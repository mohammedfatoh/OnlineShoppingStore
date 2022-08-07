using Microsoft.AspNetCore.Mvc;
using OnlineShoppingStore.Models;
using OnlineShoppingStore.Services;
using System.Diagnostics;

namespace OnlineShoppingStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceBase<Category> categoryService;


        public HomeController(ILogger<HomeController> logger,
            IServiceBase<Category> categoryService)
        {
            _logger = logger;
            this.categoryService = categoryService;
        }

        public IActionResult Index()
        {
            List<string> Namescategories = new List<string>();
            foreach (var category in categoryService.GetAll())
            {
                Namescategories.Add(category.Name);
            }
            ViewBag.NamesCategories = Namescategories;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}