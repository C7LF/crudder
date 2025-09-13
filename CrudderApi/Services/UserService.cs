using CrudderApi.Data;
using CrudderApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudderApi.Services
{
    public class UserService(TodoContext context)
    {
        private readonly TodoContext _context = context;
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddRefreshTokenAsync(int userId, string token, DateTime expires)
        {
            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = token,
                Expires = expires,
                IsRevoked = false,
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }

        // Get user associated with a valid refresh token
        public async Task<User?> GetUserByRefreshTokenAsync(string token)
        {
            var refreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token 
                                           && !rt.IsRevoked 
                                           && rt.Expires > DateTime.UtcNow);

            if (refreshToken == null) return null;

            return await _context.Users.FindAsync(refreshToken.UserId);
        }

        // Replace old refresh token with a new one (rotation)
        public async Task ReplaceRefreshTokenAsync(int userId, string oldToken, string newToken, DateTime newExpires)
        {
            var existingToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.UserId == userId && rt.Token == oldToken);

            if (existingToken != null)
            {
                existingToken.IsRevoked = true;
                await _context.SaveChangesAsync();
            }

            await AddRefreshTokenAsync(userId, newToken, newExpires);
        }

        // Revoke a refresh token (logout)
        public async Task RevokeRefreshTokenAsync(string token)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
            if (refreshToken != null)
            {
                refreshToken.IsRevoked = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}