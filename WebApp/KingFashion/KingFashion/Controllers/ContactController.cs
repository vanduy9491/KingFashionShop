using KingFashion.Commons;
using KingFashion.Helpers;
using KingFashion.Models.Contacts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace KingFashion.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("Contact/Get")]
        public async Task<IActionResult> Get()
        {
            var data = await ApiHelper.HttpGet<List<Contact>>(@$"{Common.ApiUrl}Contact");
            return Ok(data);
        }
        [HttpGet]
        [Route("Contact/ViewContact/{id}")]
        public async Task<IActionResult> ViewContact( int id)
        {
            ViewBag.ContactId = id;
            var data = await ApiHelper.HttpGet<Contact>(@$"{Common.ApiUrl}Contact/Get/{id}");
            ViewBag.To = data.Email;
            return View(data);
        }
        
        [HttpGet]
        public async Task<IActionResult> Reply()
        {
            return View();
        }
        [HttpPost]
        [Route("Contact/Reply")]
        public IActionResult RepLy([FromBody] EmailModel model)
        {
            using (MailMessage message = new MailMessage(model.FromEmail, model.To))
            {
                message.Subject = model.Subject;
                message.Body = model.Body;
                message.IsBodyHtml = false;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential cred = new NetworkCredential(model.FromEmail, model.FromPassword);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = cred;
                    smtp.Port = 587;
                    smtp.Send(message);
                    ViewBag.Message = "Email sent Successfully!";
                }
            }
            return View();
        }
        [HttpPut]
        [Route("ChangeStatus")]
        public async  Task<IActionResult> ChangeStatus([FromBody] ChangeStatusContact model)
        {
            return Ok(await ApiHelper.HttpPost<Contact>(@$"{Common.ApiUrl}Contact/ChangStatus/{model.Id}","PUT", model));
        }
        
    }
}
