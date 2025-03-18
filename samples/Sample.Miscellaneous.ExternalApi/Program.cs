using BitToLife.Miscellaneous.ExternalApi.Apple;
using BitToLife.Miscellaneous.ExternalApi.Meta;
using BitToLife.Miscellaneous.ExternalApi.OIDC;

// Apple OIDC
string appleIssuer = "https://appleid.apple.com";
string appleAudience = ""; // Apple Bundle ID or Services ID
string appleIdToken = ""; // Apple ID Token
AppleAuthKeyService appleAuthKeyService = new(new HttpClient());
AppleIdTokenValidator appleTokenValidator = new(appleAuthKeyService);
TokenValidationResult appleTokenValidationResult = await appleTokenValidator.ValidateAsync(appleIdToken, appleIssuer, appleAudience);
if (appleTokenValidationResult.IsValid)
{
    AppleUserInfo appleUser = AppleUserInfo.GetUserInfo(appleTokenValidationResult.ClaimsPrincipal!);
}

// Facebook OIDC
string facebookIssuer = "https://www.facebook.com";
string facebookAudience = ""; // Facebook App ID
string facebookLimitedAccessToken = ""; // Facebook Limited User Access Token
FacebookAuthKeyService facebookAuthKeyService = new(new HttpClient());
FacebookTokenValidator facebookTokenValidator = new(facebookAuthKeyService);
TokenValidationResult facebookTokenValidationResult = await facebookTokenValidator.ValidateAsync(facebookLimitedAccessToken, facebookIssuer, facebookAudience);
if (facebookTokenValidationResult.IsValid)
{
    FacebookUserInfo facebookUser = FacebookUserInfo.GetUserInfo(facebookTokenValidationResult.ClaimsPrincipal!);
}

// Facebook Classic
string facebookClassicAccessToken = ""; // Facebook Classic User Access Token
FacebookUserService facebookUserService = new(new HttpClient());
FacebookApiResult<FacebookUserInfo> facebookUserResult = await facebookUserService.GetUserInfoAsync(facebookClassicAccessToken, [.. FacebookUserService.DEFAULT_PUBLIC_PROFILE_FIELDS, "email"]);

Console.ReadLine();