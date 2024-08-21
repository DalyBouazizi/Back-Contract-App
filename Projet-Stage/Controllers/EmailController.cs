using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;


namespace Projet_Stage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost("SendEmail")]
        public IActionResult SendEmail(string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Sebn TN2 ", "sebn@sebn.tn"));
            email.To.Add(MailboxAddress.Parse("bouazizimedali99@gmail.com"));
            email.Subject = "Contract end in one month";
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();

            try
            {
                smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate("bouazizimedali50@gmail.com", "hozv rqjr dcfz svmd");
                smtp.Send(email);
                smtp.Disconnect(true);
                return Ok("Email sent successfully");
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error sending email: {ex.Message}");
            }
        }


    }
}
