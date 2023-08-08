using CoffeePricingMgt;
using CoffeePricingMgt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CoffeePricingMgt.Models;

namespace CoffeePricingMgt.Controllers
{
    public class AuthController : Controller
    {
        DataContext db = new DataContext();


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(tblUser model, string command)
        {
            try
            {
                
                var User = db.tblUsers.Where(u => u.FullName == model.FullName).FirstOrDefault();
                if (User != null)
                {
                    var abc = HelperClass.Decrypt(User.Password);
                    if (HelperClass.Decrypt(User.Password) == model.Password)
                    {
                        if (command == "Login")
                        {
                            FormsAuthentication.SetAuthCookie(User.Id.ToString(), false);
                            Session["UserId"] = User.Id.ToString();
                            if (User.Role == "Admin")
                            {
                                Session["Role"] = "Admin";
                                return RedirectToAction("Index", "ProductPricings");
                            }
                            else
                            {
                                Session["Role"] = "Normal";
                                return RedirectToAction("ReportView", "Prices");
                            }
                            
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Login Failed! You have entered wrong password.";
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("Logout")]
        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            Session["UserId"] = null;
            Session["Role"] = null;
            return Redirect("Login");
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgetPassword(tblUser model, string command)
        {
            try
            {
                var User = db.tblUsers.Where(u => u.Email == model.Email).FirstOrDefault();
                if (User != null)
                {
                    var encryptedData = HelperClass.Encrypt(User.Id.ToString());
                    var body = "<p>Email From: </p><p>Message:</p><p>{2}</p> Click link : ";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(model.Email));  // replace with valid value 
                    message.From = new MailAddress("admin@lab1472.com");  // replace with valid value
                    message.Subject = "Your email subject";
                    message.Body = body + Environment.NewLine + Environment.NewLine + "https://attendancems.lab1472.com/Auth/ResetPassword/" + encryptedData;
                    message.IsBodyHtml = true;

                    using (var smtp = new SmtpClient())
                    {
                        smtp.UseDefaultCredentials = false;
                        var credential = new NetworkCredential
                        {
                            UserName = "admin@lab1472.com",  // replace with valid value
                            Password = "3hM!7d8y"  // replace with valid value
                        };
                        smtp.Credentials = credential;
                        smtp.Host = "mail.lab1472.com";
                        smtp.Port = 25;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.EnableSsl = false;

                        smtp.Send(message);
                    }

                    return View(model);
                }
                else
                {
                    TempData["Message"] = "Email sending failed! Email not found.";
                }

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error! " + ex.Message;
                return View(model);
            }
        }

        public ActionResult RequestCredentials()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendCredentialsRequest(tblRequestCredential model)
        {
            // Send an email to the specified address with the provided details
            var body = $"Name: {model.Name}<br/>Company: {model.Company}<br/>Email: {model.Email}";
            var message = new MailMessage();
            message.To.Add(new MailAddress("willem@sacofgroup.com")); // Change to the correct email address
            message.From = new MailAddress("willem@sacofgroup.com"); // Change to the correct from address
            message.Subject = "Login Credentials Request";
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient("mail.sacofgroup.com"))
            {
                smtp.EnableSsl = true;
                //smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Port = 587;// 465;
                smtp.Credentials = new NetworkCredential("willem@sacofgroup.com", "ceqbo7-jitgaq-goRdik"); // Replace with your Gmail credentials   
                smtp.Send(message);
            }

            TempData["Message"] = "Credentials request sent successfully.";
            return RedirectToAction("Login");
        }

    }
}