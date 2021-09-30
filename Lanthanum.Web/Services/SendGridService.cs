using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;
using Lanthanum.Data.Domain;
using Lanthanum.Web.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
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

        public async Task SendWelcomeEmailAsync(User user, string callbackUrl)
        {
            var dynamicTemplateData = new Dictionary<string, string>
            {
                {"date", DateTime.Today.ToString("dd MMMM yyyy")},
                {"site-link", callbackUrl}
            };

            var message = MailHelper.CreateSingleTemplateEmail(
                _senderEmail,
                new EmailAddress(user.Email), 
                _options.Value.WelcomeMailTemplateId, 
                dynamicTemplateData
                );
            
            var response = await _client.SendEmailAsync(message);
            if (response.IsSuccessStatusCode)
            {
                // TODO: Add logging
            }
        }

        public async Task SendResetPasswordRequestAsync(User user, string resetUrl)
        {
            //
        }

        private string GetBaseUrl()
        {
            return $"{_httpContextAccessor.HttpContext?.Request.Scheme}://" +
                   $"{_httpContextAccessor.HttpContext?.Request.Host.ToUriComponent()}";
        }
    }
}