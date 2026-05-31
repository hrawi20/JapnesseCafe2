using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JapnesseCafe.Models;

namespace JapnesseCafe.Controllers
{
    public class AdminController : Controller
    {
        private readonly CafeDbContext db = new CafeDbContext();

        public ActionResult Index()
        {
            ViewBag.MenuCount = db.MenuItems.Count();
            ViewBag.ProductCount = db.Products.Count();
            return View();
        }

        public ActionResult MenuItems()
        {
            return View(db.MenuItems.OrderBy(x => x.Category).ThenBy(x => x.Name).ToList());
        }

        public ActionResult CreateMenuItem()
        {
            return View(new MenuItem { IsAvailable = true, CreatedAt = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMenuItem(MenuItem item, HttpPostedFileBase imageFile)
        {
            item.CreatedAt = DateTime.Now;
            ReadImage(imageFile, item);
            if (ModelState.IsValid)
            {
                db.MenuItems.Add(item);
                db.SaveChanges();
                return RedirectToAction("MenuItems");
            }
            return View(item);
        }

        public ActionResult EditMenuItem(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var item = db.MenuItems.Find(id);
            if (item == null) return HttpNotFound();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMenuItem(MenuItem item, HttpPostedFileBase imageFile)
        {
            var existing = db.MenuItems.Find(item.Id);
            if (existing == null) return HttpNotFound();
            if (ModelState.IsValid)
            {
                existing.Name = item.Name;
                existing.Description = item.Description;
                existing.Price = item.Price;
                existing.Category = item.Category;
                existing.IsAvailable = item.IsAvailable;
                ReadImage(imageFile, existing);
                db.SaveChanges();
                return RedirectToAction("MenuItems");
            }
            return View(item);
        }

        public ActionResult DeleteMenuItem(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var item = db.MenuItems.Find(id);
            if (item == null) return HttpNotFound();
            return View(item);
        }

        [HttpPost, ActionName("DeleteMenuItem")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMenuItemConfirmed(int id)
        {
            var item = db.MenuItems.Find(id);
            if (item != null)
            {
                db.MenuItems.Remove(item);
                db.SaveChanges();
            }
            return RedirectToAction("MenuItems");
        }

        public ActionResult Products()
        {
            return View(db.Products.OrderBy(x => x.Category).ThenBy(x => x.Name).ToList());
        }

        public ActionResult CreateProduct()
        {
            return View(new Product { IsAvailable = true, CreatedAt = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(Product product, HttpPostedFileBase imageFile)
        {
            product.CreatedAt = DateTime.Now;
            ReadImage(imageFile, product);
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Products");
            }
            return View(product);
        }

        public ActionResult EditProduct(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var product = db.Products.Find(id);
            if (product == null) return HttpNotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(Product product, HttpPostedFileBase imageFile)
        {
            var existing = db.Products.Find(product.Id);
            if (existing == null) return HttpNotFound();
            if (ModelState.IsValid)
            {
                existing.Name = product.Name;
                existing.Description = product.Description;
                existing.Price = product.Price;
                existing.Category = product.Category;
                existing.IsAvailable = product.IsAvailable;
                ReadImage(imageFile, existing);
                db.SaveChanges();
                return RedirectToAction("Products");
            }
            return View(product);
        }

        public ActionResult DeleteProduct(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var product = db.Products.Find(id);
            if (product == null) return HttpNotFound();
            return View(product);
        }

        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProductConfirmed(int id)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
            return RedirectToAction("Products");
        }

        private static void ReadImage(HttpPostedFileBase imageFile, MenuItem item)
        {
            if (imageFile == null || imageFile.ContentLength <= 0) return;
            using (var reader = new BinaryReader(imageFile.InputStream))
            {
                item.ImageData = reader.ReadBytes(imageFile.ContentLength);
                item.ImageMimeType = imageFile.ContentType;
            }
        }

        private static void ReadImage(HttpPostedFileBase imageFile, Product product)
        {
            if (imageFile == null || imageFile.ContentLength <= 0) return;
            using (var reader = new BinaryReader(imageFile.InputStream))
            {
                product.ImageData = reader.ReadBytes(imageFile.ContentLength);
                product.ImageMimeType = imageFile.ContentType;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
