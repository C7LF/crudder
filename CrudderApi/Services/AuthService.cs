using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CrudderApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CrudderApi.Services
{
    public class AuthService(IConfiguration config)
    {
        private readonly PasswordHasher<User> _passwordHasher = new();
        private readonly string _jwtKey = config["Jwt:Key"] ?? throw new Exception("JWT Key missing!");

        public string HashPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public bool VerifyPassword(User user, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result != PasswordVerificationResult.Failed;
        }

        public string GenerateJwt(User user)
        {
            // Convert the secret key string into a byte array
            var key = Encoding.UTF8.GetBytes(_jwtKey);

            // Create a token handler which will generate the JWT
            var tokenHandler = new JwtSecurityTokenHandler();

            // Define the token details
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // The subject contains the claims that will be stored in the token
                Subject = new ClaimsIdentity(
                [
                    // Unique identifier for the user
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

                    // Username of the user
                    new Claim(ClaimTypes.Name, user.Username)
                ]),

                // Set token expiration (here, 1 hour from now)
                Expires = DateTime.UtcNow.AddHours(1),

                // Specify how the token will be signed
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), // Use symmetric key
                    SecurityAlgorithms.HmacSha256Signature // Use HMAC SHA256 algorithm
                )
            };

            // Create the token using the token handler and descriptor
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return the serialized JWT as a string
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

    }
}