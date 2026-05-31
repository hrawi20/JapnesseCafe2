using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using JapnesseCafe.Models;

namespace JapnesseCafe.Controllers
{
    public class ProductsController : Controller
    {
        private readonly CafeDbContext db = new CafeDbContext();

        public ActionResult Index()
        {
            var products = db.Products
                .Where(x => x.IsAvailable)
                .OrderBy(x => x.Category)
                .ThenBy(x => x.Name)
                .ToList();
            return View(products);
        }

        public ActionResult Image(int id)
        {
            var product = db.Products.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (product == null || product.ImageData == null || product.ImageData.Length == 0)
            {
                return HttpNotFound();
            }
            return File(product.ImageData, product.ImageMimeType ?? "image/png");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
