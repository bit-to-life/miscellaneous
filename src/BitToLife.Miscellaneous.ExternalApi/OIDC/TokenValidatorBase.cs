using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BitToLife.Miscellaneous.ExternalApi.OIDC;

public abstract class TokenValidatorBase(AuthKeyServiceBase authKeyService)
{
    public async Task<TokenValidationResult> ValidateAsync(string token, string issuer, string audience, CancellationToken cancellationToken = default)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        TokenValidationParameters validationParameters = await GetValidationParameters(issuer, audience, cancellationToken);

        try
        {
            ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);

            return new TokenValidationResult
            {
                IsValid = true,
                ClaimsPrincipal = claimsPrincipal,
            };
        }
        catch (Exception ex)
        {
            return new TokenValidationResult
            {
                IsValid = false,
                ErrorMessage = ex.Message
            };
        }
    }

    private async Task<TokenValidationParameters> GetValidationParameters(string issuer, string audience, CancellationToken cancellationToken)
    {
        JsonWebKeySet keySet = await authKeyService.GetKeySetAsync(cancellationToken);

        return new TokenValidationParameters
        {
            IssuerSigningKeys = keySet.Keys,
            ValidIssuer = issuer,
            ValidAudience = audience
        };
    }
}
