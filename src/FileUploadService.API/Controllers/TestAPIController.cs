using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Sendmail.Models;
using Sendmail.Interface;

namespace FileUploadService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAPIController : ControllerBase
    {
        private readonly ISendmailAsync _sendmailAsync;

        public TestAPIController(ISendmailAsync sendmailAsync)
        {
            _sendmailAsync = sendmailAsync;
        }
        [HttpGet]
        [Authorize]
        public string TestApi()
        {
            Log.Information("TestApi");
            // test email
            //var message = new Message(new string[] { "nonthagornlaor@gmail.com" }, "Testemail Notification", "This is the content from our email.");
            //_sendmailAsync.SendEmailAsync(message);
            return "TestApi";
        }
    }
}
