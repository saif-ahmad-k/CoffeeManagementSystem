using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.Entity;
using System.Net;
using CoffeePricingMgt.Models;
using Microsoft.Reporting.WinForms;

namespace CoffeePricingMgt.Controllers
{
    public class ReportingController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            return View();
        }
        public FileResult ExportReportByReportData(string ReportName, string ReportType, bool Landscape = false)
        {
            LocalReport localReport = new LocalReport();
            DataSet ReportSet = new DataSet();
            string ReportNameClassLibrary3 = ReportName;
            try
            {
                ReportSet = TempData["ReportData"] as DataSet;
                string ClientShortName;
                try
                {
                    ReportName = ReportName + ".rdlc";
                }
                catch (Exception ex)
                {
                }
                ReportNameClassLibrary3 = ReportNameClassLibrary3 + ".rdlc";

                var relativePath = Server.MapPath("~/Reports/" + ReportName);
                if ((System.IO.File.Exists(relativePath)))
                    localReport.ReportPath = Server.MapPath("~/Reports/" + ReportName);
                else
                    localReport.ReportPath = Server.MapPath("~/Reports/" + ReportNameClassLibrary3);

                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet1";
                reportDataSource.Value = ReportSet.Tables[0];
                //localReport.EnableExternalImages = true;
                localReport.DataSources.Add(reportDataSource);
                string mimeType = "";
                string extenssion = "";

                if (ReportType.ToUpper() == "EXCEL")
                {
                    mimeType = "application/vnd.ms-excel";
                    extenssion = "xls";
                }
                else if (ReportType.ToUpper() == "PDF")
                {
                    mimeType = "application/pdf";
                    extenssion = "pdf";
                }
                else
                {
                    mimeType = "application/pdf";
                    extenssion = "pdf";
                }

                string PageHeight = "8.5in";
                string PageWidth = "11in";

                if (Landscape)
                {
                    PageHeight = "29.7cm";
                    PageWidth = "21cm";
                }

                string encoding;
                string fileNameExtension;
                // The DeviceInfo settings should be changed based on the reportType            
                // http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
                string deviceInfo = (Convert.ToString("<DeviceInfo>" + "  <OutputFormat>") + mimeType) + "</OutputFormat>" + "  <PageWidth>" + PageHeight + "</PageWidth>" + "  <PageHeight>" + PageWidth + "</PageHeight>" + "  <MarginTop>0.5in</MarginTop>" + "  <MarginLeft>0.2in</MarginLeft>" + "  <MarginRight>0.2in</MarginRight>" + "  <MarginBottom>0.5in</MarginBottom>" + "</DeviceInfo>";
                Warning[] warnings;
                string[] streams;

                byte[] renderedBytes = localReport.Render(ReportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);


                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                byte[] bytes = encoding.GetBytes("Please re generate report " + ex.Message);
                return File(bytes, "application/pdf");
            }

            return null/* TODO Change to default(_) if this is not a reference type */;
        }
        public ActionResult ExportVenderAggingReport()
        {
            LocalReport localReport = new LocalReport();
            DataSet ReportSet = new DataSet();
            ReportSet = TempData["ReportData"] as DataSet;
            string reportName = "ClientRecordReport.rdlc";
            string ClientShortName;
            localReport.ReportPath = Server.MapPath("~/Reports/" + reportName);

                ReportDataSource reportDataSource = new ReportDataSource();
          


            reportDataSource.Name = "DataSet1";

            reportDataSource.Value = ReportSet.Tables[0];

            // Dim dm As New DataModel
            // reportDataSource.Value = dm.GetInvoiveInfo().Tables(0)

            localReport.EnableExternalImages = true;

                localReport.DataSources.Add(reportDataSource);

                // Export(localReport)
                // Print()

                // Dim imagePath As String = New Uri(Server.MapPath("~/Content/assets/images/ClientLogo.jpg")).AbsoluteUri
                // Dim parameter As New ReportParameter("LogoPath", imagePath)
                // localReport.SetParameters(parameter)
                // localReport.Refresh()

                string reportType = "Excel";
                string mimeType = "application/vnd.ms-excel";
                string encoding;
                string fileNameExtension;
                // The DeviceInfo settings should be changed based on the reportType            
                // http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
                string deviceInfo = (Convert.ToString("<DeviceInfo>" + "  <OutputFormat>") + "application/vnd.ms-excel") + "</OutputFormat>" + "  <PageWidth>8.5in</PageWidth>" + "  <PageHeight>11in</PageHeight>" + "  <MarginTop>0.5in</MarginTop>" + "  <MarginLeft>0.2in</MarginLeft>" + "  <MarginRight>0.2in</MarginRight>" + "  <MarginBottom>0.5in</MarginBottom>" + "</DeviceInfo>";
                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes = localReport.Render(reportType, deviceInfo,out mimeType,out encoding,out fileNameExtension,out streams, out warnings);

                string filename = string.Format("{0}.{1}", "ExportToExcel", "xls");
                Response.ClearHeaders();
                Response.Clear();
                Response.AddHeader("Content-Disposition", Convert.ToString("attachment;filename=") + filename);
                Response.ContentType = mimeType;
                Response.BinaryWrite(renderedBytes);
                Response.Flush();
                Response.End();
            


            return View();
        }

    }
}