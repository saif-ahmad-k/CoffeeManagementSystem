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
using Rotativa;
using Rotativa.Options;

namespace CoffeePricingMgt.Controllers
{
    [SessionTimeout]
    public class PricesController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Prices
        public async Task<ActionResult> Index()
        {
            var vm = new ProductPricingForListVM();
            List<SelectListItem> lesse = new List<SelectListItem>() {
    new SelectListItem {
        Text = "-", Value = "0"
    }
};
            var lesseList = db.tblProducts.ToList();
            foreach (var it in lesseList)
            {
                var newitem = new SelectListItem
                {
                    Text = it.Name,
                    Value = it.ID.ToString()
                };
                lesse.Add(newitem);
            }

            ViewBag.ProductID = lesse;

            vm.ProductPricingList = await db.tblProductPricings.Include(t => t.tblProduct).OrderByDescending(p => p.PricingDate).ToListAsync();
            return View(vm);
        }
        [HttpPost]
        public async Task<ActionResult> Index(ProductPricingForListVM vm)
        {
            var result = await db.tblProductPricings.Include(t => t.tblProduct).ToListAsync();
            if (vm.ProductID != null && vm.ProductID != 0)
            {
                result = result.Where(p => p.ProductID == vm.ProductID).OrderByDescending(p => p.PricingDate).ToList();
            }
            if (vm.FromDate != null && vm.ToDate != null)
            {
                result = result.Where(p => p.PricingDate >= vm.FromDate && p.PricingDate <= vm.ToDate).OrderByDescending(p => p.PricingDate).ToList();
            }
            vm.ProductPricingList = result;
            List<SelectListItem> lesse = new List<SelectListItem>() {
    new SelectListItem {
        Text = "-", Value = "0"
    }
};
            var lesseList = db.tblProducts.ToList();
            foreach (var it in lesseList)
            {
                var newitem = new SelectListItem
                {
                    Text = it.Name,
                    Value = it.ID.ToString()
                };
                lesse.Add(newitem);
            }

            ViewBag.ProductID = lesse;
            return View(vm);
        }
        public async Task<ActionResult> RecordReport(int? ProductId)
        {
            //DateTime? FromDate, DateTime? ToDate, int? SerialNumber, int? DoctorId, int? PatientId
            ProcuduresHelper rm = new ProcuduresHelper();
            //DateTime FDate = DateTime.MinValue;
            //DateTime TDate = DateTime.MinValue;
            //int SNumber = 0;
            //int DId = 0;
            //int PId = 0;
            //if (FromDate != DateTime.MinValue && ToDate != DateTime.MinValue && FromDate != null && ToDate != null)
            //{
            //    FDate = Convert.ToDateTime(FromDate);
            //    TDate = Convert.ToDateTime(ToDate);
            //}
            //if (SerialNumber != null && SerialNumber != 0)
            //{
            //    SNumber = Convert.ToInt32(SerialNumber);
            //}
            //if (DoctorId != null && DoctorId != 0)
            //{
            //    DId = Convert.ToInt32(DoctorId);
            //}
            //if (PatientId != null && PatientId != 0)
            //{
            //    PId = Convert.ToInt32(PatientId);
            //}
            DataSet dataset = rm.GetFOBOfferList();
            TempData["ReportData"] = dataset;
            return RedirectToAction("ExportReportByReportData", "Reporting", new { ReportName = "FOBOfferList", ReportType = "pdf", Landscape = true });
        }
        public async Task<ActionResult> ReportView()
        {
            ProcuduresHelper rm = new ProcuduresHelper();
            DataSet dataset = rm.GetFOBOfferList();
            return View(dataset);
        }
        public ActionResult ReportViewPartial()
        {
            ProcuduresHelper rm = new ProcuduresHelper();
            DataSet dataset = rm.GetFOBOfferList();

            return new PartialViewAsPdf("_ReportView", dataset)
            {
                PageSize = Size.A4,
                FileName = "OfferList.pdf"
            };
            //return View(dataset);
        }
    }
}