using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace User
{
    public class UserService
    {
        private readonly UserRepository _repository;

        public UserService(UserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<User> RegisterUserAsync(string username, string email, string password)
        {
            var existing = await _repository.GetByUsernameAsync(username);
            if (existing != null)
                throw new InvalidOperationException("Username already exists.");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(user);
            return user;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _repository.GetByUsernameAsync(username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;
            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            await _repository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}

