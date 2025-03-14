using Microsoft.AspNetCore.Builder;                
using Microsoft.Extensions.DependencyInjection;   
using Microsoft.Extensions.Hosting;                
using Microsoft.AspNetCore.Components.Authorization; 
using Syncfusion.Blazor;                           
using Microsoft.AspNetCore.SignalR;               
using MudBlazor.Services;                         
using Application.DependencyInjection;            
// using Infrastructure.DependencyInjection;         
using WebUI.States;                              
using WebUI.Services;                             
using WebUI.Interfaces;  
using WebUI.Hubs;                             

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
// builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
// builder.Services.AddSingleton<ChangePasswordState>();
// builder.Services.AddScoped<UserActiveOrderCountState>();
// builder.Services.AddScoped<AdminActiveOrderCountState>();
// builder.Services.AddScoped<GenericHomeHeaderState>();
// builder.Services.AddScoped<NetcodeHubHeaderState>();
// builder.Services.AddScoped<NetcodeHubConnectionService>();
// builder.Services.AddScoped<ICustomAuthorizationService, CustomAuthorizationService>();
// builder.Services.AddSyncfusionBlazor();
// builder.Services.AddVirtualizationService();
// builder.Services.AddNetcodeHubLocalStorageService();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("LICENSE KEY"); //TODO!!
builder.Services.AddMudServices();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub(); // Map Blazor hub
app.MapHub<YourSignalRHub>("/signalrHub");

app.Run();