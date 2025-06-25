using ServiceManagement.Domain.Entities;

namespace ServiceManagement.Domain.Interfaces;

public interface ITokenGenerator
{
    public string GenerateToken(User user);
}
