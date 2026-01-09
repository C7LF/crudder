using Crudder.Application.Auth.Dtos;
using Crudder.Application.Common.Interfaces;
using Crudder.Domain.Interfaces;
using MediatR;

namespace Crudder.Application.Auth.Commands.RefreshToken;

public class RefreshTokenHandler(
    IUserRepository userRepository,
    IJwtService jwtService
) : IRequestHandler<RefreshTokenCommand, AuthResultDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtService _jwtService = jwtService;

    public async Task<AuthResultDto> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByRefreshTokenAsync(request.RefreshToken);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid refresh token");

        var newJwt = _jwtService.GenerateJwt(user);
        var newRefreshToken = _jwtService.GenerateRefreshToken();

        await _userRepository.ReplaceRefreshTokenAsync(
            user.Id,
            request.RefreshToken,
            newRefreshToken,
            DateTime.UtcNow.AddDays(7));

        return new AuthResultDto(
            user.Id,
            user.Email,
            newJwt,
            newRefreshToken
        );
    }
}
