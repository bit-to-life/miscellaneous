using BitToLife.Miscellaneous.ExternalApi.OIDC;

namespace BitToLife.Miscellaneous.ExternalApi.Apple;

public sealed class AppleIdTokenValidator(AppleAuthKeyService authKeyService) : TokenValidatorBase(authKeyService)
{
}
