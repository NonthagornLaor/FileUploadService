using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;
using FileUploadService.API.Models;
using Microsoft.AspNetCore.Authorization;
using Sendmail.Interface;
using Sendmail.Models;
using System.Text.Json;
using Newtonsoft.Json;

namespace FileUploadService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly ISendmailAsync _sendmailAsync;

        public FileUploadController(ISendmailAsync sendmailAsync)
        {
            _sendmailAsync = sendmailAsync;
        }
        [HttpPost]
        [Authorize]
        public IActionResult UploadFile()//[FromForm] IList<IFormFile> files
        {
            try
            {
                if (!Request.Form.Files.Any())
                    return Ok(ResponseModels.NotFound());

                var resfilenameList = new List<FileUploadResponse>();
                var resfilename = new FileUploadResponse();
                string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "FileUpload");
                if (!Directory.Exists(pathToSave))
                    Directory.CreateDirectory(pathToSave);

                foreach (IFormFile file in Request.Form.Files)
                {
                    resfilename = new FileUploadResponse();
                    resfilename.filename = file.FileName;
                    resfilenameList.Add(resfilename);

                    string fullPath = Path.Combine(pathToSave, file.FileName);
                    using FileStream stream = new(fullPath, FileMode.Create);
                    file.CopyTo(stream);
                }

                // sendmail notification
                var jsonContent = JsonConvert.SerializeObject(resfilenameList);
                var message = new Message(new string[] { "nonthagornlaor@gmail.com" },
                                                         "UploadFile Notification",
                                                         jsonContent);
                _sendmailAsync.SendEmailAsync(message);
                return Ok(ResponseModels.Success(resfilenameList));
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"UploadFile Error {this.GetType().Name}.{nameof(UploadFile)}");
                return Ok(ResponseModels.Error());
            }
        }
    }
}
