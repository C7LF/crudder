
using Crudder.Domain.Entities;

namespace Crudder.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateJwt(User user);
    string GenerateRefreshToken();
}
