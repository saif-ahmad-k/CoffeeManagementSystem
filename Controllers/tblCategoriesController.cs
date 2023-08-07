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
    public class tblCategoriesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: tblCategories
        public async Task<ActionResult> Index()
        {
            var tblCategories = db.tblCategories.Include(t => t.tblUser);
            return View(await tblCategories.ToListAsync());
        }

        // GET: tblCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategory tblCategory = await db.tblCategories.FindAsync(id);
            if (tblCategory == null)
            {
                return HttpNotFound();
            }
            return View(tblCategory);
        }

        // GET: tblCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Sequance")] tblCategory tblCategory)
        {
            if (ModelState.IsValid)
            {
                tblCategory.UserID = new Guid(Session["UserId"].ToString());
                db.tblCategories.Add(tblCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblCategory);
        }

        // GET: tblCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategory tblCategory = await db.tblCategories.FindAsync(id);
            if (tblCategory == null)
            {
                return HttpNotFound();
            }
            return View(tblCategory);
        }

        // POST: tblCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Sequance")] tblCategory tblCategory)
        {
            if (ModelState.IsValid)
            {
                tblCategory.UserID = new Guid(Session["UserId"].ToString());
                db.Entry(tblCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblCategory);
        }

        // GET: tblCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategory tblCategory = await db.tblCategories.FindAsync(id);
            if (tblCategory == null)
            {
                return HttpNotFound();
            }
            return View(tblCategory);
        }

        // POST: tblCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblCategory tblCategory = await db.tblCategories.FindAsync(id);
            db.tblCategories.Remove(tblCategory);
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
