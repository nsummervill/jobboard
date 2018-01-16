using System.Web.Mvc;
using JobBoard.UI.MVC.Models;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;

namespace JobBoard.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel contact)
        {
            //Create the body of the email
            string body = string.Format("Name: {0}<br />Email: {1}<br />Subject: {2}<br />Message: {3}", contact.Name, contact.Email, contact.Subject, contact.Message);
            Debug.WriteLine(body);
            //Create and configure a MailMessage object
            MailMessage msg = new MailMessage("no-reply@summdev.com", "nsummervill@yahoo.com", "Email", body);
            //Configure msg
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;
            //Create and configure an SmtpClient object
            SmtpClient client = new SmtpClient("mail.summdev.com");
            client.Credentials = new NetworkCredential("no-reply@summdev.com", "Skater09!");
            //Send the email
            using (client)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        client.Send(msg);
                        //Step 7: Create ContactConfirmation View
                        //(Right clicked Views folder and added a View()
                        return View("ContactConfirm");
                    }

                }
                catch
                {
                    ViewBag.ErrorMessage = "There was an error sending your email." + " Please try again";

                }

                return View(contact);


            }
        }
    }
}
