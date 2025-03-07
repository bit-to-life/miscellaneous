using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BitToLife.Miscellaneous.ExternalApi.Apple;

public sealed class AppleIdTokenValidator(AppleAuthKeyService authKeyService)
{
    private readonly AppleAuthKeyService _authKeyService = authKeyService;

    public async Task<AppleTokenValidationResult> ValidateAsync(string idToken, string validAudience, CancellationToken cancellationToken = default)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        TokenValidationParameters validationParameters = await GetValidationParameters(validAudience, cancellationToken);

        try
        {
            ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(idToken, validationParameters, out _);

            return new AppleTokenValidationResult(true, claimsPrincipal, null);
        }
        catch (Exception ex)
        {
            return new AppleTokenValidationResult(false, null, ex.Message);
        }
    }

    private async Task<TokenValidationParameters> GetValidationParameters(string validAudience, CancellationToken cancellationToken)
    {
        JsonWebKeySet keySet = await _authKeyService.GetKeySetAsync(cancellationToken);

        return new TokenValidationParameters
        {
            IssuerSigningKeys = keySet.Keys,
            ValidAudience = validAudience,
            ValidIssuer = AppleAuthKeyService.BASE_ADDRESS
        };
    }
}
