using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using JapnesseCafe.Models;

namespace JapnesseCafe.Controllers
{
    public class MenuController : Controller
    {
        private readonly CafeDbContext db = new CafeDbContext();

        public ActionResult Index()
        {
            var items = db.MenuItems
                .Where(x => x.IsAvailable)
                .OrderBy(x => x.Category)
                .ThenBy(x => x.Name)
                .ToList();
            return View(items);
        }

        public ActionResult Image(int id)
        {
            var item = db.MenuItems.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (item == null || item.ImageData == null || item.ImageData.Length == 0)
            {
                return HttpNotFound();
            }
            return File(item.ImageData, item.ImageMimeType ?? "image/png");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
