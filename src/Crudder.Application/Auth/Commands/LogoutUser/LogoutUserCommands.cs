using MediatR;

namespace Crudder.Application.Auth.Commands.LogoutUser;

public record LogoutUserCommand(string RefreshToken)
    : IRequest;
