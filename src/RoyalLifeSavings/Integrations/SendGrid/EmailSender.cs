using Microsoft.Extensions.Options;
using RoyalLifeSavings.Integrations.SendGrid;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace RoyalLifeSavings.Integrations
{
    public class EmailSender : IEmailSender
    {
        private readonly ISendGridClient _sendGrid;
        private readonly ILogger<IEmailSender> _logger;
        private readonly EmailOptions _options;

        public EmailSender(ISendGridClient sendGrid, IOptions<EmailOptions> options, ILogger<IEmailSender> logger)
        {
            _sendGrid = sendGrid;
            _logger = logger;
            _options = options.Value;
        }

        public async Task SendEmailAsync(string email, string subject = null, string body = null,
            object templateData = null)
        {
            var message = new SendGridMessage { From = new EmailAddress(_options.FromEmail, _options.FromName) };

            if (templateData is not null)
            {
                message.TemplateId = _options.TemplateId;
                message.SetTemplateData(templateData);
            }
            else
            {
                message.SetSubject(subject);
                message.HtmlContent = body;
            }

            message.AddTo(new EmailAddress(email));
            message.SetClickTracking(false, false);

            try
            {
                var response = await _sendGrid.SendEmailAsync(message);
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Email sent. Status code: {StatusCode}", response.StatusCode);
                }
                else
                {
                    var res = await response.Body.ReadAsStringAsync();
                    _logger.LogWarning("Email failed. Status code: {statusCode}. Message: {message}",
                        response.StatusCode, res);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(SendEmailAsync));
            }
        }
    }
}
