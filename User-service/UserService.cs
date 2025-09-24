using UserService.Models;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _repository.GetByUsernameAsync(username);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<User> CreateUserAsync(User user)
    {
        user.Id = Guid.NewGuid();
        user.CreatedAt = DateTime.UtcNow;
        await _repository.AddAsync(user);
        return user;
    }

    public async Task<User?> UpdateUserAsync(Guid id, User user)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return null;

        existing.Username = user.Username;
        existing.Email = user.Email;
        existing.PasswordHash = user.PasswordHash;
        await _repository.UpdateAsync(existing);
        return existing;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}

