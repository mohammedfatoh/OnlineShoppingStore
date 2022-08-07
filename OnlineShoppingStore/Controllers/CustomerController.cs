using Microsoft.AspNetCore.Mvc;
using OnlineShoppingStore.Models;
using OnlineShoppingStore.Services;

namespace OnlineShoppingStore.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IServiceBase<Category> categoryService;
        private readonly IServiceBase<Product> productService;


        public CustomerController(IServiceBase<Category> categoryService, 
            IServiceBase<Product> productService)
            
        {
            this.productService = productService;
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

        public IActionResult productsOfCategory(string categoryName)
        {
            List<string> Namescategories = new List<string>();
            foreach (var category in categoryService.GetAll())
            {
                Namescategories.Add(category.Name);
            }
            ViewBag.NamesCategories = Namescategories;
            return View((productService.GetAll().Where(p => p.Category.Name == categoryName)).ToList());
        }
    }
}
