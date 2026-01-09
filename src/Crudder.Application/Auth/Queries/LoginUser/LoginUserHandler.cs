using Crudder.Application.Auth.Dtos;
using Crudder.Application.Common.Interfaces;
using Crudder.Domain.Interfaces;
using MediatR;

namespace Crudder.Application.Auth.Queries.LoginUser;

public class LoginUserHandler(
    IUserRepository userRepository,
    IJwtService jwtService,
    IPasswordHasher passwordHasher
) : IRequestHandler<LoginUserQuery, AuthResultDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtService _jwtService = jwtService;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<AuthResultDto> Handle(
        LoginUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null || !_passwordHasher.Verify(user, request.Password))
            throw new UnauthorizedAccessException("Invalid credentials");

        var jwt = _jwtService.GenerateJwt(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        await _userRepository.AddRefreshTokenAsync(
            user.Id,
            refreshToken,
            DateTime.UtcNow.AddDays(7));

        return new AuthResultDto(
            user.Id,
            user.Email,
            jwt,
            refreshToken
        );
    }
}