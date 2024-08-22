using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using Projet_Stage.Models;
using Projet_Stage.Services.Classes;
using Projet_Stage.Services.Interfaces;


namespace Projet_Stage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly  IEmailService emailService;

        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }


        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailModel emailModel)
        {
            bool res = false;
            try
            {
                res = await this.emailService.SendEmailAsync(emailModel.to, emailModel.subject, emailModel.body);
                if (res == false)
                {  
                    return BadRequest("Error sending email");
                }
                else
                {
                    return Ok("Email sent successfully");
                }

            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
            
            
        }


    }
}
