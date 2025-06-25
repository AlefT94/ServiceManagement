using ServiceManagement.Domain.Entities;

namespace ServiceManagement.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}
