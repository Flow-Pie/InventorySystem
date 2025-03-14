using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.DataAccess;
using Application.Extension.Identity;
using Application.Interfaces.Identity;
using Application.Handlers; // Add this using directive
using MediatR;

public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        // Register DbContext with SQL Server
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")),
            ServiceLifetime.Scoped
        );

        // Register DbContextFactory (if needed)
        services.AddDbContextFactory<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")),
            ServiceLifetime.Scoped
        );

        // Configure Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        }).AddIdentityCookies();

        // Configure Identity Core
        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        // Configure Authorization Policies
        services.AddAuthorizationBuilder()
            .AddPolicy("AdministrationPolicy", adp =>
            {
                adp.RequireAuthenticatedUser();
                adp.RequireRole("Admin", "Manager");
            })
            .AddPolicy("UserPolicy", adp =>
            {
                adp.RequireAuthenticatedUser();
                adp.RequireRole("User");
            });

        // Add Cascading Authentication State (for Blazor)
        services.AddCascadingAuthenticationState();

        // Register Account custom service
        services.AddScoped<IAccount, Application.Services.Identity.Account>();

        // Register MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateProductHandler).Assembly));

        return services;
    }
}