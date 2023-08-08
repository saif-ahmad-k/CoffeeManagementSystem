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
using static System.Data.Entity.Infrastructure.Design.Executor;
using System.Runtime.Remoting.Lifetime;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Xml;
using HtmlAgilityPack;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using System.Text;
using iTextSharp.tool.xml.html;
using Rotativa;
using Rotativa.Options;

namespace CoffeePricingMgt.Controllers
{
    [SessionTimeout]
    
        public class ProductPricingsController : Controller
        {
            private DataContext db = new DataContext();

            // GET: ProductPricings
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
                List<SelectListItem> lesse2 = new List<SelectListItem>() {
    new SelectListItem {
        Text = "-", Value = "0"
    }
};
                ViewBag.TermID = lesse2;
                var lesseList2 = db.tblTerms.ToList();
                foreach (var it in lesseList2)
                {
                    var newitem = new SelectListItem
                    {
                        Text = it.Name,
                        Value = it.ID.ToString()
                    };
                    lesse2.Add(newitem);
                }

                ViewBag.TermID = lesse2;
                vm.ProductPricingList = await db.tblProductPricings.Where(p => p.IsActive == true).Include(t => t.tblProduct).OrderByDescending(p => p.PricingDate).ToListAsync();
                return View(vm);
            }
            [HttpPost]
            public async Task<ActionResult> Index(ProductPricingForListVM vm)
            {
                var result = await db.tblProductPricings.Where(p => p.IsActive == true).Include(t => t.tblProduct).ToListAsync();
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
                List<SelectListItem> lesse2 = new List<SelectListItem>() {
    new SelectListItem {
        Text = "-", Value = "0"
    }
};
                ViewBag.TermID = lesse2;
                var lesseList2 = db.tblTerms.ToList();
                foreach (var it in lesseList2)
                {
                    var newitem = new SelectListItem
                    {
                        Text = it.Name,
                        Value = it.ID.ToString()
                    };
                    lesse2.Add(newitem);
                }

                ViewBag.TermID = lesse2;
                return View(vm);
            }


            // GET: ProductPricings/Details/5
            public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProductPricing tblProductPricing = await db.tblProductPricings.FindAsync(id);
            if (tblProductPricing == null)
            {
                return HttpNotFound();
            }
            return View(tblProductPricing);
        }

        // GET: ProductPricings/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.tblCategories.OrderBy(p => p.Sequance), "ID", "Name");
            ViewBag.ProductID = new SelectList(db.tblProducts, "ID", "Name");

            List<SelectListItem> ShippingPeriodItem = new List<SelectListItem>() {
    new SelectListItem {
        Text = "-", Value = ""
                        }
                            };
            var ShippingPeriodList = db.tblShippingPeriods.ToList();
            foreach (var it in ShippingPeriodList)
            {
                var newitem = new SelectListItem
                {
                    Text = it.Name,
                    Value = it.ID.ToString()
                };
                ShippingPeriodItem.Add(newitem);
            }
            ViewBag.ShippingPeriodID = ShippingPeriodItem;

            ViewBag.TermID = new SelectList(db.tblTerms, "ID", "Name");
            ViewBag.CropID = new SelectList(db.tblCrops, "ID", "CropName");
            //        List<SelectListItem> TermItem = new List<SelectListItem>() {
            //new SelectListItem {
            //    Text = "-", Value = ""
            //                    },new SelectListItem {
            //    Text = "FOT", Value = "FOT"
            //                    },new SelectListItem {
            //    Text = "FOB", Value = "FOB"
            //                    }
            //                        };
            //ViewBag.Notes = TermItem;
            return View();
        }

        // POST: ProductPricings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,CreatedDate,PricingDate,CropID,Price,TermID,ProductID,ShippingPeriodID,CategoryID,Bags")] tblProductPricing tblProductPricing)
        {
            if (ModelState.IsValid)
            {
                tblProductPricing.CreatedDate = DateTime.Now;
                tblProductPricing.PricingDate = DateTime.Now;
                tblProductPricing.UserID = new Guid(Session["UserId"].ToString());
                tblProductPricing.IsActive = true;

                //int bags = Convert.ToInt32(Request.Form["Bags"]);
                //tblProductPricing.Bags = bags;
                //tblProductPricing.CategoryID = tblProductPricing.tblProduct.CategoryID;
                db.tblProductPricings.Add(tblProductPricing);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblProductPricing.CategoryID);
            ViewBag.ProductID = new SelectList(db.tblProducts, "ID", "Name", tblProductPricing.ProductID);
            ViewBag.TermID = new SelectList(db.tblTerms, "ID", "Name", tblProductPricing.TermID);
            ViewBag.CropID = new SelectList(db.tblCrops, "ID", "CropName", tblProductPricing.CropID);
            return View(tblProductPricing);
        }

        // GET: ProductPricings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProductPricing tblProductPricing = await db.tblProductPricings.FindAsync(id);
            if (tblProductPricing == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.tblProducts, "ID", "Name", tblProductPricing.ProductID);
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblProductPricing.tblProduct.CategoryID);


            List<SelectListItem> ShippingPeriodItem = new List<SelectListItem>() {
    new SelectListItem {
        Text = "-", Value = ""
                        }
                            };
            var ShippingPeriodList = db.tblShippingPeriods.ToList();
            foreach (var it in ShippingPeriodList)
            {
                var newitem = new SelectListItem
                {
                    Text = it.Name,
                    Value = it.ID.ToString()
                };
                ShippingPeriodItem.Add(newitem);
            }
            ViewBag.ShippingPeriodID = new SelectList(ShippingPeriodItem, "Value", "Text", tblProductPricing.ShippingPeriodID);
            ViewBag.TermID = new SelectList(db.tblTerms, "ID", "Name", tblProductPricing.TermID);
            ViewBag.CropID = new SelectList(db.tblCrops, "ID", "CropName", tblProductPricing.CropID);
            //        List<SelectListItem> TermItem = new List<SelectListItem>() {
            //new SelectListItem {
            //    Text = "-", Value = ""
            //                    },new SelectListItem {
            //    Text = "FOT", Value = "FOT"
            //                    },new SelectListItem {
            //    Text = "FOB", Value = "FOB"
            //                    }
            //                        };
            //ViewBag.Notes = new SelectList(TermItem, "Value", "Text", tblProductPricing.Notes);
            return View(tblProductPricing);
        }

        // POST: ProductPricings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,CreatedDate,PricingDate,CropID,Price,Notes,ProductID,ShippingPeriodID,CategoryID,Bags")] tblProductPricing tblProductPricing)
        {
            if (ModelState.IsValid)
            {
                tblProductPricing.UserID = new Guid(Session["UserId"].ToString());
                tblProductPricing.CategoryID = tblProductPricing.CategoryID;
                tblProductPricing.IsActive = true;
                db.Entry(tblProductPricing).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.tblCategories, "ID", "Name", tblProductPricing.CategoryID);
            ViewBag.ProductID = new SelectList(db.tblProducts, "ID", "Name", tblProductPricing.ProductID);
            ViewBag.TermID = new SelectList(db.tblTerms, "ID", "Name", tblProductPricing.TermID);
            ViewBag.CropID = new SelectList(db.tblCrops, "ID", "CropName", tblProductPricing.CropID);
            return View(tblProductPricing);
        }

        // GET: ProductPricings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProductPricing tblProductPricing = await db.tblProductPricings.FindAsync(id);
            if (tblProductPricing == null)
            {
                return HttpNotFound();
            }
            return View(tblProductPricing);
        }

        // POST: ProductPricings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblProductPricing tblProductPricing = await db.tblProductPricings.FindAsync(id);
            tblProductPricing.IsActive = false;
            db.Entry(tblProductPricing).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> DeleteAll()
        {
            foreach(var item in db.tblProductPricings.Where(p=>p.IsActive==true).ToList())
            {
                item.IsActive = false;
                db.Entry(item).State = EntityState.Modified;
            }
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
        [HttpGet]
        public JsonResult GetProductByCategory(int CategoryID)
        {
            var abc = Json(db.tblProducts.Where(p => p.CategoryID == CategoryID).Select(x => new {x.ID,x.Name,x.CategoryID }).ToList(), JsonRequestBehavior.AllowGet);
            return abc;
        }

        //[HttpPost]
        //[ValidateInput(false)]
        //public FileResult Export(string ExportData)
        //{
        //    HtmlNode.ElementsFlags["img"] = HtmlElementFlag.Closed;
        //    HtmlNode.ElementsFlags["input"] = HtmlElementFlag.Closed;
        //    HtmlDocument doc = new HtmlDocument();
        //    doc.OptionFixNestedTags = true;
        //    doc.LoadHtml(ExportData);
        //    ExportData = doc.DocumentNode.OuterHtml;
        //    //using (MemoryStream stream = new System.IO.MemoryStream())
        //    //{
        //    //    StringReader reader = new StringReader(ExportData);
        //    //    Document PdfFile = new Document(PageSize.A4);
        //    //    PdfWriter writer = PdfWriter.GetInstance(PdfFile, stream);
        //    //    PdfFile.Open();
        //    //    XMLWorkerHelper.GetInstance().ParseXHtml(writer, PdfFile, reader);
        //    //    PdfFile.Close();
        //    //    return File(stream.ToArray(), "application/pdf", "ExportData.pdf");
        //    //}

        //    //List<string> cssFiles = new List<string>();
        //    //cssFiles.Add(@"/Content/assets/css/pdfstyles.css");

        //    //var output = new MemoryStream();

        //    //var input = new MemoryStream(Encoding.UTF8.GetBytes(ExportData));

        //    //var document = new Document();
        //    //var writer = PdfWriter.GetInstance(document, output);
        //    //writer.CloseStream = false;

        //    //document.Open();
        //    //var htmlContext = new HtmlPipelineContext(null);
        //    //htmlContext.SetTagFactory(iTextSharp.tool.xml.html.Tags.GetHtmlTagProcessorFactory());

        //    //ICSSResolver cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);
        //    //cssFiles.ForEach(i => cssResolver.AddCssFile(System.Web.HttpContext.Current.Server.MapPath(i), true));

        //    //var pipeline = new CssResolverPipeline(cssResolver, new HtmlPipeline(htmlContext, new PdfWriterPipeline(document, writer)));
        //    //var worker = new XMLWorker(pipeline, true);
        //    //var p = new XMLParser(worker);
        //    //p.Parse(input);
        //    //document.Close();
        //    //output.Position = 0;


        //    //Response.Clear();
        //    //Response.ContentType = "application/pdf";
        //    //Response.AddHeader("Content-Disposition", "attachment; filename=ExportData.pdf");
        //    //Response.BinaryWrite(output.ToArray());
        //    //// myMemoryStream.WriteTo(Response.OutputStream); //works too
        //    //Response.Flush();
        //    //Response.Close();
        //    //Response.End();
        //    //Byte[] res = null;
        //    //using (MemoryStream ms = new MemoryStream())
        //    //{
        //    //    var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
        //    //    pdf.Save(ms);
        //    //    res = ms.ToArray();
        //    //}
        //    //return res;
        //    return File(output.ToArray(), "application/pdf", "ExportData.pdf");
        //}
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
