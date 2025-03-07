namespace BitToLife.Miscellaneous.Cryptography;

/// <summary>
/// RSA 키 쌍.
/// </summary>
/// <param name="PublicKey"></param>
/// <param name="PublicKeyPem"></param>
/// <param name="PrivateKey"></param>
/// <param name="PrivateKeyPem"></param>
public sealed record RSAKeyPair(byte[] PublicKey, string PublicKeyPem, byte[] PrivateKey, string PrivateKeyPem);