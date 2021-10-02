#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanthanum.Web.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Lanthanum.Web.Services
{
    public class SendGridService: IEmailSenderService
    {
        private readonly IOptions<SendGridOptions> _options;
        private readonly SendGridClient _client;
        private readonly EmailAddress _senderEmail;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SendGridService(IOptions<SendGridOptions> options, IHttpContextAccessor httpContextAccessor)
        {
            _options = options;
            _client = new SendGridClient(_options.Value.ApiKey);
            _senderEmail = new EmailAddress(_options.Value.SenderEmail, _options.Value.SenderName);
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendWelcomeEmailAsync(string recipientEmail, string callbackUrl)
        {
            var dynamicTemplateData = new Dictionary<string, string>
            {
                {"date", DateTime.Today.ToString("dd MMMM yyyy")},
                {"site-link", callbackUrl}
            };

            var message = MailHelper.CreateSingleTemplateEmail(
                _senderEmail,
                new EmailAddress(recipientEmail), 
                _options.Value.WelcomeEmailTemplateId, 
                dynamicTemplateData
                );
            
            var response = await _client.SendEmailAsync(message);
            if (response.IsSuccessStatusCode)
            {
                throw new SendGridException("Email not sent successfully. Not success status code.");
            }
        }

        public async Task SendResetPasswordRequestAsync(string recipientEmail, string resetUrl)
        {
            var dynamicTemplateData = new Dictionary<string, string>
            {
                {"site-link", resetUrl}
            };
            
            var message = MailHelper.CreateSingleTemplateEmail(
                _senderEmail,
                new EmailAddress(recipientEmail), 
                _options.Value.ResetPasswordTemplateId, 
                dynamicTemplateData
            );
            
            var response = await _client.SendEmailAsync(message);
            if (response.IsSuccessStatusCode)
            {
                throw new SendGridException("Email not sent successfully. Not success status code.");
            }
        }
    }

    class SendGridException : Exception
    {
        public SendGridException(string? message) : base(message)
        {
        }
    }
}