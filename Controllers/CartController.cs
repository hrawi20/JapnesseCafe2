using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using JapnesseCafe.Models;

namespace JapnesseCafe.Controllers
{
    public class CartController : Controller
    {
        private readonly CafeDbContext db = new CafeDbContext();
        private const string CartSessionKey = "CafeCart";

        public ActionResult Index()
        {
            return View(GetCart());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMenuItem(int id)
        {
            var item = db.MenuItems.Find(id);
            if (item != null && item.IsAvailable)
            {
                AddItem("Menu Item", item.Id, item.Name, item.Price);
                TempData["Message"] = item.Name + " was added to your cart.";
            }
            return RedirectToAction("Index", "Menu");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(int id)
        {
            var product = db.Products.Find(id);
            if (product != null && product.IsAvailable)
            {
                AddItem("Product", product.Id, product.Name, product.Price);
                TempData["Message"] = product.Name + " was added to your cart.";
            }
            return RedirectToAction("Index", "Products");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(string key)
        {
            var cart = GetCart();
            var existing = cart.FirstOrDefault(x => x.CartKey == key);
            if (existing != null)
            {
                cart.Remove(existing);
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout()
        {
            SaveCart(new List<CartItem>());
            return RedirectToAction("CheckoutSuccess");
        }

        public ActionResult CheckoutSuccess()
        {
            return View();
        }

        private void AddItem(string type, int id, string name, decimal price)
        {
            var cart = GetCart();
            var key = type + "-" + id;
            var existing = cart.FirstOrDefault(x => x.CartKey == key);
            if (existing == null)
            {
                cart.Add(new CartItem { CartKey = key, ItemId = id, ItemType = type, Name = name, Price = price, Quantity = 1 });
            }
            else
            {
                existing.Quantity++;
            }
            SaveCart(cart);
        }

        private List<CartItem> GetCart()
        {
            var cart = Session[CartSessionKey] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
                SaveCart(cart);
            }
            return cart;
        }

        private void SaveCart(List<CartItem> cart)
        {
            Session[CartSessionKey] = cart;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
