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
    public class CropController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Products
        public async Task<ActionResult> Index()
        {
            var tblCrops = db.tblCrops.Include(t => t.tblUser);
            return View(await tblCrops.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCrop tblCrops = await db.tblCrops.FindAsync(id);
            if (tblCrops == null)
            {
                return HttpNotFound();
            }
            return View(tblCrops);
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
        public async Task<ActionResult> Create([Bind(Include = "CropName")] tblCrop tblCrops)
        {
            if (ModelState.IsValid)
            {
                tblCrops.UserID = new Guid(Session["UserId"].ToString());
                db.tblCrops.Add(tblCrops);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tblCrops);
        }


        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCrop tblCrops = await db.tblCrops.FindAsync(id);
            if (tblCrops == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblTerms.CategoryID);
            return View(tblCrops);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,CropName,UserID")] tblCrop tblCrops)
        {
            if (ModelState.IsValid)
            {
                tblCrops.UserID = new Guid(Session["UserId"].ToString());
                db.Entry(tblCrops).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblProduct.CategoryID);
            return View(tblCrops);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCrop tblCrops = await db.tblCrops.FindAsync(id);
            if (tblCrops == null)
            {
                return HttpNotFound();
            }
            return View(tblCrops);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblCrop tblCrop = await db.tblCrops.FindAsync(id);
            db.tblCrops.Remove(tblCrop);
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
