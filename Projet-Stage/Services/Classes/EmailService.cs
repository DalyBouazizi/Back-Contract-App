using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using Projet_Data.Repo.Interfaces;
using Projet_Stage.Services.Interfaces;
using MailKit.Net.Smtp;

namespace Projet_Stage.Services.Classes
{
    public class EmailService : IEmailService
    {

        private readonly IContractRepository _contractRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public EmailService(IContractRepository contractRepository, IEmployeeRepository employeeRepository)
        {
            _contractRepository = contractRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> SendEmailAsync(IEnumerable<string> toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Sebn TN2", "sebn@sebn.tn"));

            foreach (var to in toEmail)
            {
                email.To.Add(MailboxAddress.Parse(to));
            }

            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();

            try
            {
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("bouazizimedali50@gmail.com", "hozv rqjr dcfz svmd");
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                throw new Exception($"Error sending email: {ex.Message}");
            }
        }
    }
    }

