using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lanthanum.Web.Domain;
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

        public async Task SendWelcomeEmailAsync(User user)
        {
            var baseUrl =
                $"{_httpContextAccessor.HttpContext?.Request.Scheme}://" +
                $"{_httpContextAccessor.HttpContext?.Request.Host.ToUriComponent()}";
            
            var dynamicTemplateData = new Dictionary<string, string>
            {
                {"date", DateTime.Today.ToString("dd MMMM yyyy")},
                {"site-link", baseUrl}
            };

            var message = MailHelper.CreateSingleTemplateEmail(
                _senderEmail,
                new EmailAddress(user.Email), 
                _options.Value.WelcomeMailTemplateId, 
                dynamicTemplateData
                );
            
            await _client.SendEmailAsync(message);
        }
    }
}