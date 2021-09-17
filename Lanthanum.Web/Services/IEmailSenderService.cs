using System.Threading.Tasks;
using Lanthanum.Web.Domain;

namespace Lanthanum.Web.Services
{
    public interface IEmailSenderService
    {
        public Task SendWelcomeEmailAsync(User user);
    }
}