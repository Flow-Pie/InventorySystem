using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebUI.Components;
using WebUI.Components.Identity;
using Application.Services;
using Application.Services.Identity;
using Application.Interfaces.Identity;
using WebUI.Components.Layout.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddInfrastructureServices(builder.Configuration);

// Register custom AuthenticationStateProvider
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthStateProvider>();

// Register IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Register IAccountService
builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

// Ensure SetUpAsync runs every time the app starts
using (var scope = app.Services.CreateScope())
{
    var accountService = scope.ServiceProvider.GetRequiredService<IAccount>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation("Setting up default admin user...");
        await accountService.SetUpAsync();
        logger.LogInformation("Default admin user setup completed.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while setting up the default admin user.");
    }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.MapSignOutEndpoint();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
