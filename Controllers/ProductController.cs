using QrCodeMenuTest.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace QrCodeMenuTest.Controllers
{
    public class ProductController : Controller
    {
        private RestaurantContext db = new RestaurantContext();

        // GET: Product
        public ActionResult Index(bool? isVegetarian, bool? isVegan, bool? isGlutenFree, bool? isSugarFree, int? categoryId)
        {
            var products = db.Products.Include(p => p.ProductCategory).AsQueryable();

            if (isVegetarian.HasValue)
                products = products.Where(p => p.IsVegetarian == isVegetarian.Value);
            if (isVegan.HasValue)
                products = products.Where(p => p.IsVegan == isVegan.Value);
            if (isGlutenFree.HasValue)
                products = products.Where(p => p.IsGlutenFree == isGlutenFree.Value);
            if (isSugarFree.HasValue)
                products = products.Where(p => p.IsSugarFree == isSugarFree.Value);
            if (categoryId.HasValue)
                products = products.Where(p => p.ProductCategoryId == categoryId.Value);

            ViewBag.ProductCategories = new SelectList(db.ProductCategories, "ProductCategoryId", "Name");
            return View(products.ToList());
        }

        // GET: Product/EditPartial/5 (for modal)
        public ActionResult EditPartial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "Name", product.ProductCategoryId);
            return PartialView("_EditPartial", product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPartial([Bind(Include = "ProductId,Name,Description,Price,IsVegetarian,IsVegan,IsGlutenFree,IsSugarFree,ProductCategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "Name", product.ProductCategoryId);
            return PartialView("_EditPartial", product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "Name");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Name,Description,Price,IsVegetarian,IsVegan,IsGlutenFree,IsSugarFree,ProductCategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "Name", product.ProductCategoryId);
            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
