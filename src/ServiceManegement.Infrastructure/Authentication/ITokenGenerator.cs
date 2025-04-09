using ServiceManagement.Domain.Entities;

namespace ServiceManegement.Infrastructure.Authentication;

public interface ITokenGenerator
{
    public string GenerateToken(User user);
}
