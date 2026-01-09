namespace Crudder.Application.Auth.Dtos
{
    /// <summary>
    /// Result returned by authentication handlers (Register/Login).
    /// </summary>
    public class AuthResultDto(int userId, string email, string jwtToken, string refreshToken)
    {
        public int UserId { get; set; } = userId;
        public string Email { get; set; } = email;
        public string JwtToken { get; set; } = jwtToken;
        public string RefreshToken { get; set; } = refreshToken;
    }
}
