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
            Product product = productService.GetDetails(productId);
            decimal newprice = product.Price;
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

                var ProductExist = productSelected.Find(p => p.Id == productId);
                if (ProductExist != null)
                {
                    ProductExist.Quantity += 1;
                    ProductExist.Price += newprice;
                }
                else
                {
                    productSelected.Add(product);
                }

                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(productSelected, Formatting.None,
                         new JsonSerializerSettings()
                         {
                             ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                         }));

                ViewBag.cart = productSelected.Count();
                int? count = HttpContext.Session.GetInt32("count");
                HttpContext.Session.SetInt32("count", (int)(count + 1));

            }
            return RedirectToAction("productsOfCategory", new { categoryName = product.Category.Name });


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

        public ActionResult RemoveProductfromCart(int productId)
        {
            Product product = productService.GetDetails(productId);
            var productSeilalized = HttpContext.Session.GetString("cart");
            var productSelected = JsonConvert.DeserializeObject<List<Product>>(productSeilalized);

            productSelected.Remove(productSelected.Find(p => p.Id == productId));


            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(productSelected, Formatting.None,
                  new JsonSerializerSettings()
                  {
                      ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                  }));

            ViewBag.cart = productSelected.Count();
            int? count = HttpContext.Session.GetInt32("count");
            if (count > 0)
                HttpContext.Session.SetInt32("count", (int)(count - 1));



            return RedirectToAction("Cart");
        }

        public ActionResult SavaProductLaterShopping(int productId)
        {
            Product product = productService.GetDetails(productId);
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Savedproducts")))
            {
                List<Product> productSelected = new List<Product>();

                productSelected.Add(product);

                HttpContext.Session.SetString("Savedproducts", JsonConvert.SerializeObject(productSelected, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));
                RemoveProductfromCart(productId);
                ViewBag.cart = productSelected.Count();
                HttpContext.Session.SetInt32("countSavedproducts", 1);


            }
            else
            {
                var productsSerialized = HttpContext.Session.GetString("Savedproducts");
                var productSelected = JsonConvert.DeserializeObject<List<Product>>(productsSerialized);


                productSelected.Add(product);

                HttpContext.Session.SetString("Savedproducts", JsonConvert.SerializeObject(productSelected, Formatting.None,
                         new JsonSerializerSettings()
                         {
                             ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                         }));
                HttpContext.Session.SetInt32("countSavedproducts", productSelected.Count());
                RemoveProductfromCart(productId);

            }
            return RedirectToAction("Cart");
        }
        public ActionResult RemoveProductfromLaterList(int productId)
        {
            Product product = productService.GetDetails(productId);
            var productSeilalized = HttpContext.Session.GetString("Savedproducts");
            var Savedproducts = JsonConvert.DeserializeObject<List<Product>>(productSeilalized);

            Savedproducts.Remove(Savedproducts.Find(p => p.Id == productId));


            HttpContext.Session.SetString("Savedproducts", JsonConvert.SerializeObject(Savedproducts, Formatting.None,
                  new JsonSerializerSettings()
                  {
                      ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                  }));


            int? count = HttpContext.Session.GetInt32("countSavedproducts");
            if (count > 0)
                HttpContext.Session.SetInt32("countSavedproducts", (int)(count - 1));

            return RedirectToAction("Cart");
        }

        public ActionResult AddToCartfromLaterList(int productId)
        {
            Product product = productService.GetDetails(productId);
            var productSeilalized = HttpContext.Session.GetString("Savedproducts");
            var Savedproducts = JsonConvert.DeserializeObject<List<Product>>(productSeilalized);

            AddToCart(productId);

            Savedproducts.Remove(Savedproducts.Find(p => p.Id == productId));

            HttpContext.Session.SetString("Savedproducts", JsonConvert.SerializeObject(Savedproducts, Formatting.None,
                  new JsonSerializerSettings()
                  {
                      ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                  }));


            int? count = HttpContext.Session.GetInt32("countSavedproducts");
            if (count > 0)
                HttpContext.Session.SetInt32("countSavedproducts", (int)(count - 1));

            return RedirectToAction("Cart");
        }


        public ActionResult IncreaseproductQuantity(int productId)
        {
            Product product = productService.GetDetails(productId);
            decimal Productprice = product.Price;
            var str = HttpContext.Session.GetString("cart");
            var productSelected = JsonConvert.DeserializeObject<List<Product>>(str);

            var ProductExist = productSelected.Find(p => p.Id == productId);
            if (ProductExist != null)
            {
                ProductExist.Quantity += 1;
                ProductExist.Price += Productprice;
            }
            else
            {
                productSelected.Add(product);
            }

            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(productSelected, Formatting.None,
                     new JsonSerializerSettings()
                     {
                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                     }));

            ViewBag.cart = productSelected.Count();
            int? count = HttpContext.Session.GetInt32("count");
            HttpContext.Session.SetInt32("count", (int)(count + 1));
            return View("Cart", productSelected);
        }

        public ActionResult DecreaseproductQuantity(int productId)
        {
            Product product = productService.GetDetails(productId);
            decimal Productprice = product.Price;
            var str = HttpContext.Session.GetString("cart");
            var productSelected = JsonConvert.DeserializeObject<List<Product>>(str);

            var ProductExist = productSelected.Find(p => p.Id == productId);
            if (ProductExist != null)
            {
                ProductExist.Quantity -= 1;
                ProductExist.Price -= Productprice;
            }

            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(productSelected, Formatting.None,
                     new JsonSerializerSettings()
                     {
                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                     }));

            ViewBag.cart = productSelected.Count();
            int? count = HttpContext.Session.GetInt32("count");
            if (count > 0)
                HttpContext.Session.SetInt32("count", (int)(count - 1));
            return View("Cart", productSelected);
        }

        public ActionResult Checkout()
        {

            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(Order order)
        {
            if(!ModelState.IsValid)
                return View(order);
            return View(order);
        }
    }
}
