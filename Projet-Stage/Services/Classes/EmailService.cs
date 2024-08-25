using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using Projet_Data.Repo.Interfaces;
using Projet_Stage.Services.Interfaces;
using MailKit.Net.Smtp;
using Projet_Data.ModelsEF;
using Projet_Stage.Models;

namespace Projet_Stage.Services.Classes
{
    public class EmailService : IEmailService
    {

        private readonly ILogger<EmailService> _logger;
        private readonly IContractService _contractService;
        private readonly IEmployeeService _employeeService;
        private readonly IUserService _userService;
        


        public EmailService(
          ILogger<EmailService> logger,
          IContractService contractService,
          IEmployeeService employeeService,
          IUserService userService
          )
        {
            _logger = logger;
            _contractService = contractService;
            _employeeService = employeeService;
            _userService = userService;
           
        }

        public async Task<bool> SendEmailAsync(IEnumerable<string> toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Sebn TN2", "sebn@sebn.com"));

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







        public async Task SendContractsEmailAndAddAlertAsync()
        {
            try
            {
                var contracts = await _contractService.GetContractsEndingInOneMonthAsync();
                var combinedData = new List<(ContractGetModel contract, EmployeeGetModel employee)>();

                foreach (var contract in contracts)
                {
                    var employee = await _employeeService.GetEmployeeByRealIdAsync(contract.EmployeeId);
                    combinedData.Add((contract, employee));
                }

                if (combinedData.Any())
                {
                    Console.WriteLine("Sending email and adding alerts...");
                    Console.WriteLine(combinedData);
                    var emailBody = FormatEmailBody(combinedData);
                    var users = await _userService.GetAllUsersAsync();
                    var recipients = users.Select(user =>
                    {
                        var nom = user.Nom.ToLower().Trim().Replace(" ", "");
                        var prenom = user.Prenom.ToLower().Trim().Replace(" ", "");
                        return $"{nom}.{prenom}@sebn.com";
                    }).ToList();
                    await SendEmailAsync(recipients, "Contracts Ending This Month", emailBody);
                  
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending contracts email and adding alerts.");
            }
        }

        private string FormatEmailBody(List<(ContractGetModel contract, EmployeeGetModel employee)> combinedData)
        {

            var body = @"
        <html>
            <body style='font-family: Arial, sans-serif; color: #333;'>
                <h2 style='color: #4CAF50;'>Contracts Ending This Month</h2>
                <table style='width: 100%; border-collapse: collapse; margin-bottom: 20px;'>
                    <thead>
                        <tr>
                            <th style='border: 1px solid #ddd; padding: 8px; background-color: #f2f2f2;'>Contract ID</th>
                            <th style='border: 1px solid #ddd; padding: 8px; background-color: #f2f2f2;'>Employee Name</th>
                            <th style='border: 1px solid #ddd; padding: 8px; background-color: #f2f2f2;'>End Date</th>
                            <th style='border: 1px solid #ddd; padding: 8px; background-color: #f2f2f2;'>Contract Type</th>
                        </tr>
                    </thead>
                    <tbody>";

            foreach (var item in combinedData)
            {
                Console.WriteLine($"Contract ID: {item.contract.id}, Employee: {item.employee.Nom} {item.employee.Prenom}, DateFin: {item.contract.DateFin}, Type: {item.contract.Type}");


                body += $@"
            <tr>
                <td style='border: 1px solid #ddd; padding: 8px;'>{item.contract.id}</td>
                <td style='border: 1px solid #ddd; padding: 8px;'>{item.employee.Nom} {item.employee.Prenom}</td>
                <td style='border: 1px solid #ddd; padding: 8px;'>{item.contract.DateFin}</td>
                <td style='border: 1px solid #ddd; padding: 8px;'>{item.contract.Type}</td>
            </tr>";
            }

            body += @"
                    </tbody>
                </table>
                <div style='text-align: center;'>
                    <a href='http://localhost:4200/AlertManagement' 
                       style='display: inline-block; padding: 10px 20px; background-color: #4CAF50; color: white; text-decoration: none; border-radius: 5px;'>
                      View More Details
                    </a>
                </div>
            </body>
        </html>";

            return body;
        }

        





    }
}

