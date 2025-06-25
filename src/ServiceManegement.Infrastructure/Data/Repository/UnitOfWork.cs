using ServiceManagement.Domain.Interfaces;
using ServiceManagement.Infrastructure.Persistence;

namespace ServiceManagement.Infrastructure.Data.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public ICompanyRepository Company { get; private set; }

    public IUserRepository User { get; private set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Company = new CompanyRepository(_context);
        User = new UserRepository(_context);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
