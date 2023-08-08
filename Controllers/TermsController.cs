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
    public class TermsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Products
        public async Task<ActionResult> Index()
        {
            var tblTerms = db.tblTerms.Include(t => t.tblUser);
            return View(await tblTerms.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTerm tblTerms = await db.tblTerms.FindAsync(id);
            if (tblTerms == null)
            {
                return HttpNotFound();
            }
            return View(tblTerms);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            //ViewBag = new SelectList(db.tblCategories, "ID", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name")] tblTerm tblTerms)
        {
            if (ModelState.IsValid)
            {
                tblTerms.UserID = new Guid(Session["UserId"].ToString());
                db.tblTerms.Add(tblTerms);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tblTerms);
        }


        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTerm tblTerms = await db.tblTerms.FindAsync(id);
            if (tblTerms == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblTerms.CategoryID);
            return View(tblTerms);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,UserID")] tblTerm tblTerms)
        {
            if (ModelState.IsValid)
            {
                tblTerms.UserID = new Guid(Session["UserId"].ToString());
                db.Entry(tblTerms).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblProduct.CategoryID);
            return View(tblTerms);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTerm tblTerms = await db.tblTerms.FindAsync(id);
            if (tblTerms == null)
            {
                return HttpNotFound();
            }
            return View(tblTerms);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblTerm tblTerms = await db.tblTerms.FindAsync(id);
            db.tblTerms.Remove(tblTerms);
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
