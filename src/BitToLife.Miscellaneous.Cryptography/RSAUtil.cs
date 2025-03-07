using System.Security.Cryptography;

namespace BitToLife.Miscellaneous.Cryptography;

/// <summary>
/// RSA 암호화 유틸리티
/// </summary>
public sealed class RSAUtil
{
    private const int DEFAULT_KEY_SIZE = 2048;

    /// <summary>
    /// RSA 키 쌍을 생성합니다.
    /// </summary>
    /// <param name="keySize">키 사이즈</param>
    /// <returns></returns>
    public static RSAKeyPair GenerateKeyPair(int keySize = DEFAULT_KEY_SIZE)
    {
        using (RSACryptoServiceProvider rsa = new(keySize))
        {
            return new RSAKeyPair(
                rsa.ExportRSAPublicKey(),
                rsa.ExportRSAPublicKeyPem(),
                rsa.ExportRSAPrivateKey(),
                rsa.ExportRSAPrivateKeyPem()
            );
        }
    }

    /// <summary>
    /// RSA 암호화기를 생성합니다.
    /// </summary>
    /// <param name="publicKey">공개키</param>
    /// <param name="padding">패딩 (기본값 PKCS1)</param>
    /// <param name="keySize">키 사이즈 (기본값 2048)</param>
    /// <returns></returns>
    public static RSAEncryptor CreateEncryptor(ReadOnlySpan<byte> publicKey, RSAEncryptionPadding? padding = null, int keySize = DEFAULT_KEY_SIZE)
    {
        return new RSAEncryptor(publicKey, keySize, padding);
    }

    /// <summary>
    /// RSA 암호화기를 생성합니다.
    /// </summary>
    /// <param name="publicKeyPem">PEM 인코딩된 공개키</param>
    /// <param name="padding">패딩 (기본값 PKCS1)</param>
    /// <param name="keySize">키 사이즈 (기본값 2048)</param>
    /// <returns></returns>
    public static RSAEncryptor CreateEncryptor(ReadOnlySpan<char> publicKeyPem, RSAEncryptionPadding? padding = null, int keySize = DEFAULT_KEY_SIZE)
    {
        return new RSAEncryptor(publicKeyPem, keySize, padding);
    }

    /// <summary>
    /// RSA 암호화기를 생성합니다.
    /// </summary>
    /// <param name="publicKeyPem">PEM 인코딩된 공개키</param>
    /// <param name="password">키 암호</param>
    /// <param name="padding">패딩 (기본값 PKCS1)</param>
    /// <param name="keySize">키 사이즈 (기본값 2048)</param>
    /// <returns></returns>
    public static RSAEncryptor CreateEncryptor(ReadOnlySpan<char> publicKeyPem, ReadOnlySpan<byte> password, RSAEncryptionPadding? padding = null, int keySize = DEFAULT_KEY_SIZE)
    {
        return new RSAEncryptor(publicKeyPem, password, keySize, padding);
    }

    /// <summary>
    /// RSA 암호화기를 생성합니다.
    /// </summary>
    /// <param name="publicKeyPem">PEM 인코딩된 공개키</param>
    /// <param name="password">키 암호</param>
    /// <param name="padding">패딩 (기본값 PKCS1)</param>
    /// <param name="keySize">키 사이즈 (기본값 2048)</param>
    /// <returns></returns>
    public static RSAEncryptor CreateEncryptor(ReadOnlySpan<char> publicKeyPem, ReadOnlySpan<char> password, RSAEncryptionPadding? padding = null, int keySize = DEFAULT_KEY_SIZE)
    {
        return new RSAEncryptor(publicKeyPem, password, keySize, padding);
    }

    /// <summary>
    /// RSA 복호화기를 생성합니다.
    /// </summary>
    /// <param name="privateKey">비밀키</param>
    /// <param name="padding">패딩 (기본값 PKCS1)</param>
    /// <param name="keySize">키 사이즈 (기본값 2048)</param>
    /// <returns></returns>
    public static RSADescryptor CreateDescryptor(ReadOnlySpan<byte> privateKey, RSAEncryptionPadding? padding = null, int keySize = DEFAULT_KEY_SIZE)
    {
        return new RSADescryptor(privateKey, keySize, padding);
    }

    /// <summary>
    /// RSA 복호화기를 생성합니다.
    /// </summary>
    /// <param name="privateKeyPem">PEM 인코딩된 비밀키</param>
    /// <param name="padding">패딩 (기본값 PKCS1)</param>
    /// <param name="keySize">키 사이즈 (기본값 2048)</param>
    /// <returns></returns>
    public static RSADescryptor CreateDescryptor(ReadOnlySpan<char> privateKeyPem, RSAEncryptionPadding? padding = null, int keySize = DEFAULT_KEY_SIZE)
    {
        return new RSADescryptor(privateKeyPem, keySize, padding);
    }

    /// <summary>
    /// RSA 복호화기를 생성합니다.
    /// </summary>
    /// <param name="privateKeyPem">PEM 인코딩된 비밀키</param>
    /// <param name="password">키 암호</param>
    /// <param name="padding">패딩 (기본값 PKCS1)</param>
    /// <param name="keySize">키 사이즈 (기본값 2048)</param>
    /// <returns></returns>
    public static RSADescryptor CreateDescryptor(ReadOnlySpan<char> privateKeyPem, ReadOnlySpan<byte> password, RSAEncryptionPadding? padding = null, int keySize = DEFAULT_KEY_SIZE)
    {
        return new RSADescryptor(privateKeyPem, password, keySize, padding);
    }

    /// <summary>
    /// RSA 복호화기를 생성합니다.
    /// </summary>
    /// <param name="privateKeyPem">PEM 인코딩된 비밀키</param>
    /// <param name="password">키 암호</param>
    /// <param name="padding">패딩 (기본값 PKCS1)</param>
    /// <param name="keySize">키 사이즈 (기본값 2048)</param>
    /// <returns></returns>
    public static RSADescryptor CreateDescryptor(ReadOnlySpan<char> privateKeyPem, ReadOnlySpan<char> password, RSAEncryptionPadding? padding = null, int keySize = DEFAULT_KEY_SIZE)
    {
        return new RSADescryptor(privateKeyPem, password, keySize, padding);
    }

    /// <summary>
    /// RSA 복호화기를 생성합니다.
    /// </summary>
    /// <param name="keySize">키 사이즈 (기본값 2048)</param>
    /// <param name="pkcs8PrivateKey">PKCS8 암호화된 비밀키</param>
    /// <param name="padding">패딩 (기본값 PKCS1)</param>
    /// <returns></returns>
    public static RSADescryptor CreateDescryptor(int keySize, ReadOnlySpan<byte> pkcs8PrivateKey, RSAEncryptionPadding? padding = null)
    {
        return new RSADescryptor(keySize, pkcs8PrivateKey, padding);
    }

    /// <summary>
    /// RSA 복호화기를 생성합니다.
    /// </summary>
    /// <param name="keySize">키 사이즈 (기본값 2048)</param>
    /// <param name="pkcs8PrivateKey">PKCS8 암호화된 비밀키</param>
    /// <param name="password">키 암호</param>
    /// <param name="padding">패딩 (기본값 PKCS1)</param>
    /// <returns></returns>
    public static RSADescryptor CreateDescryptor(int keySize, ReadOnlySpan<byte> pkcs8PrivateKey, ReadOnlySpan<byte> password, RSAEncryptionPadding? padding = null)
    {
        return new RSADescryptor(keySize, pkcs8PrivateKey, password, padding);
    }

    /// <summary>
    /// RSA 복호화기를 생성합니다.
    /// </summary>
    /// <param name="keySize">키 사이즈 (기본값 2048)</param>
    /// <param name="pkcs8PrivateKey">PKCS8 암호화된 비밀키</param>
    /// <param name="password">키 암호</param>
    /// <param name="padding">패딩 (기본값 PKCS1)</param>
    /// <returns></returns>
    public static RSADescryptor CreateDescryptor(int keySize, ReadOnlySpan<byte> pkcs8PrivateKey, ReadOnlySpan<char> password, RSAEncryptionPadding? padding = null)
    {
        return new RSADescryptor(keySize, pkcs8PrivateKey, password, padding);
    }
}

