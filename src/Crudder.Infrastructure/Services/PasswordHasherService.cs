using Crudder.Application.Common.Interfaces;
using Crudder.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Crudder.Infrastructure.Services
{
    public class PasswordHasherService : IPasswordHasher
    {
        private readonly PasswordHasher<User> _hasher = new();

        public string Hash(User user, string password)
        {
            return _hasher.HashPassword(user, password);
        }

        public bool Verify(string hash, string password)
        {
            var user = new User { PasswordHash = hash };
            var result = _hasher.VerifyHashedPassword(user, hash, password);
            return result != PasswordVerificationResult.Failed;
        }

        public bool Verify(User user, string password)
        {
            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result != PasswordVerificationResult.Failed;
        }
    }
}
