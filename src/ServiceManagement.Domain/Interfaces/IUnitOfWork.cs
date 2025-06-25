namespace ServiceManagement.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICompanyRepository Company { get; }
    IUserRepository User { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
