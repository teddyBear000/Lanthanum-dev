using System.Threading.Tasks;
using Lanthanum.Data.Domain;

namespace Lanthanum.Web.Services
{
    public interface IEmailSenderService
    {
        public Task SendWelcomeEmailAsync(User user, string callbackUrl);
        public Task SendResetPasswordRequestAsync(User user, string resetUrl);
    }
}