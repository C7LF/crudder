using Crudder.Application.Auth.Commands.LogoutUser;
using Crudder.Application.Auth.Commands.RefreshToken;
using Crudder.Application.Auth.Commands.RegisterUser;
using Crudder.Application.Auth.Dtos;
using Crudder.Application.Auth.Queries.LoginUser;
using CrudderApi.Dtos.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crudder.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        

        private void SetAuthCookies(AuthResultDto result)
        {
            Response.Cookies.Append("jwt", result.JwtToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(15)
            });

            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _mediator.Send(new RegisterUserCommand(
                request.Username,
                request.Email,
                request.Password
            ));

            return Ok(new { message = "User registered successfully" });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            var result = await _mediator.Send(
                new LoginUserQuery(dto.Email, dto.Password));

            SetAuthCookies(result);
            return Ok();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized();

            var result = await _mediator.Send(
                new RefreshTokenCommand(refreshToken));

            SetAuthCookies(result);

            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (!string.IsNullOrEmpty(refreshToken))
            {
                await _mediator.Send(new LogoutUserCommand(refreshToken));
            }

            Response.Cookies.Delete("jwt");
            Response.Cookies.Delete("refreshToken");

            return Ok(new { message = "Logged out successfully" });
        }

        [HttpGet("debug")]
        public IActionResult Debug()
        {
            return Ok(new
            {
                User.Identity?.IsAuthenticated,
                Claims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }
    };
}