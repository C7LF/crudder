using Crudder.Domain.Entities;

namespace Crudder.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(int id);

    Task AddAsync(User user);

    Task AddRefreshTokenAsync(int userId, string token, DateTime expiresAt);
    Task<User?> GetByRefreshTokenAsync(string refreshToken);
    Task ReplaceRefreshTokenAsync(
        int userId,
        string oldToken,
        string newToken,
        DateTime expiresAt);

    Task RevokeRefreshTokenAsync(string refreshToken);
}
