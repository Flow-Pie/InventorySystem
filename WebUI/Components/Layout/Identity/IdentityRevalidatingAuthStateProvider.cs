using Application.Extension.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace WebUI.Components.Identity
{
    internal sealed class IdentityRevalidatingAuthStateProvider : RevalidatingAuthStateProvider<IdentityRevalidatingAuthStateProvider>
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IOptions<IdentityOptions> _options;

        public IdentityRevalidatingAuthStateProvider(
            ILoggerFactory loggerFactory,
            IServiceScopeFactory scopeFactory,
            IOptions<IdentityOptions> options)
            : base(loggerFactory)
        {
            _scopeFactory = scopeFactory;
            _options = options;
        }

        protected override TimeSpan RevalidationInterval => TimeSpan.FromSeconds(20);

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();

            var user = httpContextAccessor.HttpContext?.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var applicationUser = await userManager.GetUserAsync(user);
            if (applicationUser == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var claims = await userManager.GetClaimsAsync(applicationUser);
            var identity = new ClaimsIdentity(claims, "Identity.Application");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            return new AuthenticationState(claimsPrincipal);
        }

        protected override async Task<bool> ValidateAuthenticationStateAsync(
            AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            return await ValidateSecurityStampAsync(userManager, authenticationState.User);
        }

        private async Task<bool> ValidateSecurityStampAsync(UserManager<ApplicationUser> userManager, ClaimsPrincipal user)
        {
            var applicationUser = await userManager.GetUserAsync(user);
            if (applicationUser == null)
            {
                return false;
            }

            // Validate the security stamp or other properties as needed
            return true;
        }
    }

}