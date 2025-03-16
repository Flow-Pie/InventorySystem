using Application.Services.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services;
public static class ServiceContainer
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        return services;
    }
}