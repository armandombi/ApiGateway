using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace PaymentApi.Core.Helpers
{
    /// <summary>
    ///     Customized authentication handler to manage authorization requests
    /// </summary>
    public class ApiAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public ApiAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return await Task.Run(() =>
            {
                if (!Request.Headers.ContainsKey("Authorization"))
                    return AuthenticateResult.Fail("Missing Authorization Header");
                Request.Headers.TryGetValue("Authorization", out var auth);

                if (!auth.First().Contains("Bearer"))
                    return AuthenticateResult.Fail("Missing Bearer in Authorization");

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, "DummyUser")
                };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            });
        }
    }
}