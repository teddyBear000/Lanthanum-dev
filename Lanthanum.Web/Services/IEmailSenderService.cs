using System.Threading.Tasks;
using Lanthanum.Data.Domain;

namespace Lanthanum.Web.Services
{
    public interface IEmailSenderService
    {
        public Task SendWelcomeEmailAsync(string recipientEmail, string callbackUrl);
        public Task SendResetPasswordRequestAsync(string recipientEmail, string resetUrl);
    }
}