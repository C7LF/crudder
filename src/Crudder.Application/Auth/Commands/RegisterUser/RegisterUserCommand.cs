using MediatR;
using Crudder.Application.Auth.Dtos;

namespace Crudder.Application.Auth.Commands.RegisterUser;

public record RegisterUserCommand(
    string Username,
    string Email,
    string Password
) : IRequest<AuthResultDto>;
