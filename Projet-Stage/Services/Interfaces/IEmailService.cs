namespace Projet_Stage.Services.Interfaces
{
    public interface IEmailService
    {

        Task<bool> SendEmailAsync(IEnumerable<string> to, string subject, string body);
    }
}
