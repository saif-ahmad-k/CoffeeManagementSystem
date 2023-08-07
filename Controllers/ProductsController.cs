using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoffeePricingMgt.Models;

namespace CoffeePricingMgt.Controllers
{
    [SessionTimeout]
    public class ProductsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Products
        public async Task<ActionResult> Index()
        {
            var tblProducts = db.tblProducts.Include(t => t.tblCategory).Include(t => t.tblUser);
            return View(await tblProducts.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = await db.tblProducts.FindAsync(id);
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,CategoryID")] tblProduct tblProduct)
        {
            if (ModelState.IsValid)
            {
                tblProduct.UserID = new Guid(Session["UserId"].ToString());
                db.tblProducts.Add(tblProduct);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblProduct.CategoryID);
            return View(tblProduct);
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = await db.tblProducts.FindAsync(id);
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblProduct.CategoryID);
            return View(tblProduct);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,CategoryID,UserID")] tblProduct tblProduct)
        {
            if (ModelState.IsValid)
            {
                tblProduct.UserID = new Guid(Session["UserId"].ToString());
                db.Entry(tblProduct).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblProduct.CategoryID);
            return View(tblProduct);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = await db.tblProducts.FindAsync(id);
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblProduct tblProduct = await db.tblProducts.FindAsync(id);
            db.tblProducts.Remove(tblProduct);
            await db.SaveChangesAsync();
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
