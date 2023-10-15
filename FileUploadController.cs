using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FileUploadAndEmailWithAttachment.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            var file = Request.Files[0]; // Assuming you're uploading a single file
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                file.SaveAs(path);
                // You can save the file path to your database or perform other operations here.
            }
            return RedirectToAction("Index"); // Redirect to the appropriate view.
        }

        [HttpPost]
        public ActionResult SendEmailWithAttachment()
        {
            var smtpClient = new SmtpClient("smtp.example.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("email@example.com", "password"),
                EnableSsl = true,
            };

            var mail = new MailMessage
            {
                From = new MailAddress("your-email@example.com"),
                Subject = "Subject",
                Body = "Email Body",
            };

            // Attach the file
            var attachment = new Attachment(Server.MapPath("~/Uploads/your-file.jpg"));
            mail.Attachments.Add(attachment);

            // Add recipient
            mail.To.Add("recipient@example.com");

            // Send the email
            smtpClient.Send(mail);

            // Redirect or return a view
            return RedirectToAction("Index");
        }
    }
}