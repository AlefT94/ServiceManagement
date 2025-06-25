using ServiceManagement.Domain.Entities;

namespace ServiceManagement.Domain.Interfaces;

public interface ICompanyRepository
{
    Task AddAsync(Company company, CancellationToken cancellationToken);
    /*Task<Company> GetByIdAsync(Guid id);
    Task<IEnumerable<Company>> GetAllAsync();
    Task UpdateAsync(Company company);
    Task DeleteAsync(Guid id);*/
}
