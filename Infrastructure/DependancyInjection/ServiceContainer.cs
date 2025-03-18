using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.DataAccess;
using Application.Extension.Identity;
using Application.Interfaces.Identity;
using MediatR;
using Infrastructure.Repository;
using Infrastructure.Repository.Products.Handlers.Products;

public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        // Register DbContext with MySQL Server
        services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(config.GetConnectionString("DefaultConnection"), 
            ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection")),
            b => b.MigrationsAssembly("Infrastructure") 
        ), ServiceLifetime.Scoped);
       

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
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdministrationPolicy", policy =>
                policy.RequireAuthenticatedUser().RequireRole("Admin", "Manager"));
            options.AddPolicy("UserPolicy", policy =>
                policy.RequireAuthenticatedUser().RequireRole("User"));
        });

        // Add Cascading Authentication State (for Blazor)
        services.AddCascadingAuthenticationState();

        // Register Account custom service
        services.AddScoped<IAccount, Account>();

        // Register MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateProductHandler).Assembly));

         // Register DbContextFactory 
        services.AddDbContextFactory<AppDbContext>(options =>
            options.UseMySql(config.GetConnectionString("DefaultConnection"), 
                ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection")),
                b => b.MigrationsAssembly("Infrastructure") 
            ),
            ServiceLifetime.Scoped 
        );

        return services;
    }
}