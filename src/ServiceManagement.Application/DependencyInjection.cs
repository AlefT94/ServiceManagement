using Microsoft.Extensions.DependencyInjection;
using ServiceManagement.Application.Company.Commands.CreateCompany;

namespace ServiceManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateCompanyCommand>());

        return services;
    }
}
