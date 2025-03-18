using BitToLife.Miscellaneous.ExternalApi.OIDC;

namespace BitToLife.Miscellaneous.ExternalApi.Meta;

public sealed class FacebookTokenValidator(FacebookAuthKeyService authKeyService) : TokenValidatorBase(authKeyService)
{
}