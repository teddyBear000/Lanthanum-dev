using System.Threading.Tasks;
using Lanthanum.Data.Domain;
using Lanthanum.Data.Repositories;

namespace Lanthanum.Web.Services
{
    public class UserService
    {
        private readonly DbRepository<User> _repository;

        public UserService(DbRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task ChangePasswordAsync(User user, string newPassword)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _repository.UpdateAsync(user);
        }

        public async Task ChangePasswordAsync(int userId, string newPassword)
        {
            var user = await _repository.GetByIdAsync(userId);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _repository.UpdateAsync(user);
        }
    }
}