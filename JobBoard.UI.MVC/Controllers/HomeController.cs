using System.Web.Mvc;

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
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ContactForm( contact)
        //{
        //    //Create the body of the email
        //    string body = string.Format("Name: {0}<br />Email: {1}", contact.Name, contact.Email);
        //    //Create and configure a MailMessage object
        //    MailMessage msg = new MailMessage("no-reply@summdev.com", "nsummervill@yahoo.com", "Email", body);
        //    //Configure msg
        //    msg.IsBodyHtml = true;
        //    msg.Priority = MailPriority.High;
        //    //Create and configure an SmtpClient object
        //    SmtpClient client = new SmtpClient("mail.summdev.com");
        //    client.Credentials = new NetworkCredential("no-reply@summdev.com", "Skater09!");
        //    //Send the email
        //    using (client)
        //    {


        //        client.Send(msg);

        //        return View();


        //    }
        }
}
