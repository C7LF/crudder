using Crudder.Domain.Entities;
using Crudder.Domain.Interfaces;
using Crudder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Crudder.Infrastructure.Repositories;

public class UserRepository(CrudderDbContext context) : IUserRepository
{
    private readonly CrudderDbContext _context = context;

    public async Task<User?> GetByEmailAsync(string email)
        => await _context.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetByIdAsync(int id)
        => await _context.Users.FindAsync(id);

    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task AddRefreshTokenAsync(
        int userId,
        string token,
        DateTime expiresAt)
    {
        _context.RefreshTokens.Add(new RefreshToken
        {
            UserId = userId,
            Token = token,
            ExpiresAt = expiresAt
        });

        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u =>
                u.RefreshTokens.Any(rt =>
                    rt.Token == refreshToken &&
                    !rt.IsRevoked &&
                    rt.ExpiresAt > DateTime.UtcNow));
    }

    public async Task ReplaceRefreshTokenAsync(
        int userId,
        string oldToken,
        string newToken,
        DateTime expiresAt)
    {
        var token = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt =>
                rt.UserId == userId && rt.Token == oldToken);

        if (token != null)
            token.IsRevoked = true;

        await AddRefreshTokenAsync(userId, newToken, expiresAt);
    }

    public async Task RevokeRefreshTokenAsync(string refreshToken)
    {
        var token = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        if (token != null)
        {
            token.IsRevoked = true;
            await _context.SaveChangesAsync();
        }
    }
}
