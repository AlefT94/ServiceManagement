using ServiceManagement.Domain.Entities;

namespace ServiceManagement.Infrastructure.Authentication;

public interface ITokenGenerator
{
    public string GenerateToken(User user);
}
