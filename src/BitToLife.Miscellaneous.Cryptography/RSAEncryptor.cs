using System.Security.Cryptography;

namespace BitToLife.Miscellaneous.Cryptography;

/// <summary>
/// RSA 암호화기.
/// </summary>
public sealed class RSAEncryptor : IDisposable
{
    private RSAEncryptor(int keySize, RSAEncryptionPadding? padding = null)
    {
        _rsa = new RSACryptoServiceProvider(keySize);
        _padding = padding ?? RSAEncryptionPadding.Pkcs1;
    }

    internal RSAEncryptor(ReadOnlySpan<byte> publicKey, int keySize, RSAEncryptionPadding? padding = null) : this(keySize, padding)
    {
        _rsa.ImportRSAPublicKey(publicKey, out _);
    }

    internal RSAEncryptor(ReadOnlySpan<char> publicKeyPem, int keySize, RSAEncryptionPadding? padding = null) : this(keySize, padding)
    {
        _rsa.ImportFromPem(publicKeyPem);
    }

    internal RSAEncryptor(ReadOnlySpan<char> publicKeyPem, ReadOnlySpan<byte> password, int keySize, RSAEncryptionPadding? padding = null) : this(keySize, padding)
    {
        _rsa.ImportFromEncryptedPem(publicKeyPem, password);
    }

    internal RSAEncryptor(ReadOnlySpan<char> publicKeyPem, ReadOnlySpan<char> password, int keySize, RSAEncryptionPadding? padding = null) : this(keySize, padding)
    {
        _rsa.ImportFromEncryptedPem(publicKeyPem, password);
    }

    private readonly RSACryptoServiceProvider _rsa;
    private readonly RSAEncryptionPadding _padding;

    /// <summary>
    /// 데이터를 암호화합니다.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public byte[] Encrypt(byte[] data)
    {
        return _rsa.Encrypt(data, _padding);
    }

    public void Dispose()
    {
        _rsa.Dispose();
    }
}
