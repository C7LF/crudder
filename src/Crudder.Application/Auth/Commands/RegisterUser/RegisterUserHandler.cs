using MediatR;
using Crudder.Application.Auth.Dtos;
using Crudder.Domain.Entities;
using Crudder.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Crudder.Application.Common.Interfaces;

namespace Crudder.Application.Auth.Commands.RegisterUser;

public class RegisterUserHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher
) : IRequestHandler<RegisterUserCommand, AuthResultDto>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<AuthResultDto> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = new User
        {
            Username = request.Username,
            Email = request.Email
        };

        user.PasswordHash = _passwordHasher.Hash(user, request.Password);

        await _userRepository.AddAsync(user);

        // No auto-login on register (cleaner separation)
        return new AuthResultDto(
            user.Id,
            user.Email,
            string.Empty,
            string.Empty
        );
    }
}