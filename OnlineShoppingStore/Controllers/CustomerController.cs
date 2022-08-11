using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        // GET: AddToCart  
        public ActionResult AddToCart(int productId)
        {
            Product product =productService.GetDetails(productId);
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("cart")))
            {
                List<Product> productSelected = new List<Product>();

                productSelected.Add(product);

                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(productSelected, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));

                ViewBag.cart = productSelected.Count();

                HttpContext.Session.SetInt32("count", 1);

            }
            else
            {
                  var str = HttpContext.Session.GetString("cart");
                 var productSelected = JsonConvert.DeserializeObject<List<Product>>(str);
                int g = productSelected.Count();


                productSelected.Add(product);

                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(productSelected, Formatting.None,
                         new JsonSerializerSettings()
                         {
                             ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                         }));

                ViewBag.cart = productSelected.Count();
                int? count = HttpContext.Session.GetInt32("count");
                HttpContext.Session.SetInt32("count", (int)(count + 1));
            }
            return RedirectToAction("productsOfCategory",new { categoryName = product.Category.Name } );


    }
        public ActionResult Cart()
        {
            
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("cart")))
            {
                var productSeilalized = HttpContext.Session.GetString("cart");
                var productSelected = JsonConvert.DeserializeObject<List<Product>>(productSeilalized);
                return View(productSelected);
            }
            return View("CartShoppingEmpty");
        }

        public ActionResult CartShoppingEmpty()
        {
            return View();
        }

        public ActionResult Checkout()
        {

            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("cart")))
            {
                var productSeilalized = HttpContext.Session.GetString("cart");
                var productSelected = JsonConvert.DeserializeObject<List<Product>>(productSeilalized);
                return View(productSelected);
            }
            return View("CartShoppingEmpty");
        }


   }
}
