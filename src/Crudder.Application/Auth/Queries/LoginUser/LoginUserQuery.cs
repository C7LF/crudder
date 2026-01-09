using Crudder.Application.Auth.Dtos;
using MediatR;

namespace Crudder.Application.Auth.Queries.LoginUser
{
    public record LoginUserQuery(string Email, string Password) : IRequest<AuthResultDto>;
}