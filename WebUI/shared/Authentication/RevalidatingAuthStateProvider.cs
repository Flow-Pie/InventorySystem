using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public abstract class RevalidatingAuthStateProvider<TProvider> : AuthenticationStateProvider
    where TProvider : RevalidatingAuthStateProvider<TProvider>
{
    private readonly ILogger<TProvider> _logger;

    protected RevalidatingAuthStateProvider(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<TProvider>();
    }

    protected abstract TimeSpan RevalidationInterval { get; }

    protected abstract Task<bool> ValidateAuthenticationStateAsync(
        AuthenticationState authenticationState, CancellationToken cancellationToken);

    //TODO!!: Implement the RevalidateAsync method
}