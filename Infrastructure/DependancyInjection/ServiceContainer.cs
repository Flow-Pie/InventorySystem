using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;        
using Microsoft.AspNetCore.Authentication;  
using Microsoft.AspNetCore.Authorization;   
using Microsoft.AspNetCore.Components.Authorization;  
using Microsoft.Extensions.Configuration;   
using Microsoft.Extensions.DependencyInjection; 
using MediatR;

using Infrastructure.DataAccess;  
using Application.Extension.Identity;  // Replace with the namespace where CreateProductHandler is defined
using Microsoft.AspNetCore.Components.Authorization;  // For AddCascadingAuthenticationState()


public static class ServiceContainer
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection"), ServiceLifetime.Scoped));
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")), 
            ServiceLifetime.Scoped
        );


        services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;

        }).AddIdentityCookies();

        services.AddIdentityCore<ApplicationUser>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders();

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

        services.AddCascadingAuthenticationState();
        services.AddScoped<Application.Interface.Identity.IAccount, Account>();
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(typeof(CreateProductHandler).Assembly));
        services.AddScoped<DataAccess.IDbContextFactory<AppDbContext>, DbContextFactory<AppDbContext>>();
        
        return services;
    
    }
}