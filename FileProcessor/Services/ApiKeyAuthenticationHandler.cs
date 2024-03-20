using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace FileProcessor.Services
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private const string ApiKeyHeaderName = "X-Api-Key";

        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<ApiKeyAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey))
            {
                return AuthenticateResult.Fail("API Key was not provided");
            }

            var configuration = Context.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration.GetValue<string>("API_KEY");

            if (!apiKey.Equals(potentialApiKey))
            {
                return AuthenticateResult.Fail("Invalid API Key provided");
            }

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, apiKey),
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
