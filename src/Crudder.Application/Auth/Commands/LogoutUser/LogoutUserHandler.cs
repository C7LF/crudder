using Crudder.Domain.Interfaces;
using MediatR;

namespace Crudder.Application.Auth.Commands.LogoutUser;

public class LogoutUserHandler(
    IUserRepository userRepository
) : IRequestHandler<LogoutUserCommand>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task Handle(
        LogoutUserCommand request,
        CancellationToken cancellationToken)
    {
        await _userRepository.RevokeRefreshTokenAsync(request.RefreshToken);
    }
}
