using MediatR;
using Crudder.Application.Auth.Dtos;

namespace Crudder.Application.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(string RefreshToken)
    : IRequest<AuthResultDto>;