namespace RoyalLifeSavings.Integrations
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject = null, string htmlBody = null, object templateData = null);
    }
}
