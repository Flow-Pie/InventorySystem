using System.Security.Claims;
using Application.Extension.Identity;
using Microsoft.AspNetCore.Identity;

namespace WebUI.Components.Layout.Identity;
internal static class SignOutEndpoint
{
    public static IEndpointConventionBuilder MapSignOutEndpoint(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);
        var accountGroup = endpoints.MapGroup("/account");
        accountGroup.MapPost("/signout", async (ClaimsPrincipal user, SignInManager<ApplicationUser> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return TypedResults.LocalRedirect("/account/login");
            });
            return accountGroup;

    }
        
}