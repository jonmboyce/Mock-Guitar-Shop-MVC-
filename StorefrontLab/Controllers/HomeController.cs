using System.Web.Mvc;
using StorefrontLab.Models;
using System;
using System.Net;
using System.Net.Mail;




namespace StoreFrontLab.Controllers
 
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
        }
        [HttpPost]
        public ActionResult Contact(ContactViewModel cvm)
        { 
            if (ModelState.IsValid)
            {
                //this is the message sent, can be made however 
                string body = $"{cvm.Name} has sent you the following message: <br/>" +
                    $"{cvm.Message} <strong>from the email address:</strong> {cvm.Email}";

                MailMessage m = new MailMessage("admin@jonmboyce.com", "jonmboyce@outlook.com",
                    cvm.Subject, body);

                m.IsBodyHtml = true; //this allows us to use the html above for the line break and strong tag
                m.Priority = MailPriority.High;//set the priority level of the email sent
                m.ReplyToList.Add(cvm.Email);//reply to the person who filled out the form, not your domail email ***ask here what's up

                SmtpClient client = new SmtpClient("mail.jonmboyce.com");//configure the mail client - work on understanding this better
                client.Credentials = new NetworkCredential("admin@jonmboyce.com", "P@ssw0rd");
                //the other port thing in case it doesn't work somewhere
                //client.Port=8889;

                try
                {
                    client.Send(m);
                }
                catch (Exception e)
                {

                    ViewBag.ErrorMessage = e.StackTrace; //what exactly is going on here?***
                }

                return View("EmailConfirmation", cvm);

            } return View("EmailConfirmation");
        }
    }
}
