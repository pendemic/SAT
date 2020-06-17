using SAT.UI.MVC.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace SAT.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }//end GEtContact

        [HttpPost]
        //public JsonResult ContactAjax(ContactViewModel cvm)
        //{
        //    string body = $"{cvm.Name} has sent you: <br />{cvm.Message} <strong>from the email address:</strong> {cvm.Email}";

        //    MailMessage m = new MailMessage("no-reply@adrianalamb.com", "Lamb_is_here@yahoo.com", cvm.Subject, cvm.Message);

        //    m.IsBodyHtml = true;

        //    //reply to the Person who filled out the form, not your domain email
        //    m.ReplyToList.Add(cvm.Email);

        //    SmtpClient client = new SmtpClient("mail.adrianalamb.com");
        //    client.Credentials = new NetworkCredential("no-reply@adrianalamb.com", "@aKBXqk3Nbb7Y");
        //    client.Port = 8889;

        //    try
        //    {
        //        client.Send(m);
        //    }
        //    catch (Exception ex)
        //    {

        //        ViewBag.Message = ex.StackTrace;
        //    }//end catch
        //    return Json(cvm);
        //}//end PostContact
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }
            string message = $"You have received an email from {cvm.Name} with the subject - {cvm.Subject}. Please respond to {cvm.Email} with your response to the following message: <br><cite>{cvm.Message}</cite>";
            MailMessage msg = new MailMessage(
                "admin@mikeoskinner.com",
                "mikeokskinner@outlook.com",
                cvm.Subject,
                message
                );
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient("mail.mikeoskinner.com");
            client.Credentials = new NetworkCredential("admin@mikeoskinner.com", "Skinner=1997");
            client.Port = 8889;
            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = $"Sorry, something went wrong. Please try again later or review the stacktrace <br>{ex.StackTrace}";
                return View(cvm);
            }
            return View("EmailConfirm", cvm);
        }
    }
}
