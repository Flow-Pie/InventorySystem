using Microsoft.AspNetCore.Builder;                // For WebApplication & CreateBuilder()
using Microsoft.Extensions.DependencyInjection;    // For IServiceCollection & Add* methods
using Microsoft.Extensions.Hosting;                // For IHostEnvironment & app.Environment
using Microsoft.AspNetCore.Components.Authorization; // For AuthenticationStateProvider
using Syncfusion.Blazor;                           // For AddSyncfusionBlazor()
using Microsoft.AspNetCore.SignalR;               // For AddSignalR()


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddSingleton<ChangePasswordState>();
builder.Services.AddScoped<UserActiveOrderCountState>();
builder.Services.AddScoped<AdminActiveOrderCountState>();
builder.Services.AddScoped<GenericHomeHeaderState>();
builder.Services.AddScoped<NetcodeHubHeaderState>();
builder.Services.AddScoped<NetcodeHubConnectionService>();
builder.Services.AddScoped<ICustomAuthorizationService, CustomAuthorizationService>();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddVirtualizationService();
builder.Services.AddNetcodeHubLocalStorageService();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR LICENSE KEY");
builder.Services.AddMudServices();
builder.Services.AddSignalR();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.Run();

