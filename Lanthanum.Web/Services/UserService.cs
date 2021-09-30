using System.Threading.Tasks;
using Lanthanum.Data.Domain;
using Lanthanum.Data.Repositories;

namespace Lanthanum.Web.Services
{
    public class UserService
    {
        private readonly DbRepository<User> _repository;

        public UserService(DbRepository<User> repository, DbRepository<ActionRequest> actionRequestRepository)
        {
            _repository = repository;
        }

        public void ChangePassword(User user, string newPassword)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        }

        public async Task ChangePassword(int userId, string newPassword)
        {
            var requestOwner = await _repository.GetByIdAsync(userId);
            requestOwner.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        }
    }
}