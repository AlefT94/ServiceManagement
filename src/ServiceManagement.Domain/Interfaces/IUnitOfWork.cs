using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICompanyRepository Company { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
