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
    public class ShippingPeriodsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: ShippingPeriods
        public async Task<ActionResult> Index()
        {
            var tblShippingPeriods = db.tblShippingPeriods.Include(t => t.tblUser);
            return View(await tblShippingPeriods.ToListAsync());
        }

        // GET: ShippingPeriods/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblShippingPeriod tblShippingPeriod = await db.tblShippingPeriods.FindAsync(id);
            if (tblShippingPeriod == null)
            {
                return HttpNotFound();
            }
            return View(tblShippingPeriod);
        }

        // GET: ShippingPeriods/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.tblUsers, "Id", "FullName");
            return View();
        }

        // POST: ShippingPeriods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,UserID")] tblShippingPeriod tblShippingPeriod)
        {
            if (ModelState.IsValid)
            {
                tblShippingPeriod.UserID = new Guid(Session["UserId"].ToString());
                db.tblShippingPeriods.Add(tblShippingPeriod);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.tblUsers, "Id", "FullName", tblShippingPeriod.UserID);
            return View(tblShippingPeriod);
        }

        // GET: ShippingPeriods/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblShippingPeriod tblShippingPeriod = await db.tblShippingPeriods.FindAsync(id);
            if (tblShippingPeriod == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.tblUsers, "Id", "FullName", tblShippingPeriod.UserID);
            return View(tblShippingPeriod);
        }

        // POST: ShippingPeriods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,UserID")] tblShippingPeriod tblShippingPeriod)
        {
            if (ModelState.IsValid)
            {
                tblShippingPeriod.UserID = new Guid(Session["UserId"].ToString());
                db.Entry(tblShippingPeriod).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.tblUsers, "Id", "FullName", tblShippingPeriod.UserID);
            return View(tblShippingPeriod);
        }

        // GET: ShippingPeriods/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblShippingPeriod tblShippingPeriod = await db.tblShippingPeriods.FindAsync(id);
            if (tblShippingPeriod == null)
            {
                return HttpNotFound();
            }
            return View(tblShippingPeriod);
        }

        // POST: ShippingPeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblShippingPeriod tblShippingPeriod = await db.tblShippingPeriods.FindAsync(id);
            db.tblShippingPeriods.Remove(tblShippingPeriod);
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
