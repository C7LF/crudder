using Crudder.Domain.Entities;

namespace Crudder.Application.Common.Interfaces;

public interface IPasswordHasher
{
    string Hash(User user, string password);
    bool Verify(string hash, string password);
    bool Verify(User user, string password);
}


