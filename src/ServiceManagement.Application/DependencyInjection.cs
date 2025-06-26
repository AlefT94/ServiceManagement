using Microsoft.Extensions.DependencyInjection;
using ServiceManagement.Application.AccountApplication.Commands;

namespace ServiceManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateAccountCommand>());

        return services;
    }
}
