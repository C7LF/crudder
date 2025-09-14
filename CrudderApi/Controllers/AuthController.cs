using CrudderApi.Dtos.Auth;
using CrudderApi.Models;
using CrudderApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrudderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AuthService authService, UserService userService) : ControllerBase
    {
        private readonly AuthService _authService = authService;
        private readonly UserService _userService = userService;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = _authService.HashPassword(new User(), request.Password)
            };

            await _userService.AddUserAsync(user);

            return Ok(new { message = "User registered successfully!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userService.GetByEmailAsync(request.Email);
            if (user == null || !_authService.VerifyPassword(user, request.Password))
                return Unauthorized("Invalid credentials");

            // Generate JWT
            var token = _authService.GenerateJwt(user);

            // Generate refresh token and save
            var refreshToken = _authService.GenerateRefreshToken();
            await _userService.AddRefreshTokenAsync(user.Id, refreshToken, DateTime.UtcNow.AddDays(7));

            // set HttpOnly cookie
            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(15)
            });

            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            return Ok(new { message = "Logged in successfully" });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken)) return Unauthorized();

            var user = await _userService.GetUserByRefreshTokenAsync(refreshToken);
            if (user == null) return Unauthorized();

            // Generate new JWT
            var newJwt = _authService.GenerateJwt(user);

            // Rotate refresh token
            var newRefreshToken = _authService.GenerateRefreshToken();
            await _userService.ReplaceRefreshTokenAsync(user.Id, refreshToken, newRefreshToken, DateTime.UtcNow.AddDays(7));

            // Update cookies
            Response.Cookies.Append("jwt", newJwt, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(15)
            });

            Response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            return Ok(new { message = "Token refreshed" });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (!string.IsNullOrEmpty(refreshToken))
            {
                await _userService.RevokeRefreshTokenAsync(refreshToken);
            }

            Response.Cookies.Delete("jwt");
            Response.Cookies.Delete("refreshToken");

            return Ok(new { message = "Logged out successfully" });
        }
    };
}