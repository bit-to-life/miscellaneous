using System.Security.Cryptography;

namespace BitToLife.Miscellaneous.Cryptography;

/// <summary>
/// RSA 복호화기.
/// </summary>
public sealed class RSADescryptor : IDisposable
{
    private RSADescryptor(int keySize, RSAEncryptionPadding? padding = null)
    {
        _rsa = new RSACryptoServiceProvider(keySize);
        _padding = padding ?? RSAEncryptionPadding.Pkcs1;
    }

    internal RSADescryptor(ReadOnlySpan<byte> privateKey, int keySize, RSAEncryptionPadding? padding = null) : this(keySize, padding)
    {
        _rsa.ImportRSAPrivateKey(privateKey, out _);
    }

    internal RSADescryptor(ReadOnlySpan<char> privateKeyPem, int keySize, RSAEncryptionPadding? padding = null) : this(keySize, padding)
    {
        _rsa.ImportFromPem(privateKeyPem);
    }

    internal RSADescryptor(ReadOnlySpan<char> privateKeyPem, ReadOnlySpan<byte> password, int keySize, RSAEncryptionPadding? padding = null) : this(keySize, padding)
    {
        _rsa.ImportFromEncryptedPem(privateKeyPem, password);
    }

    internal RSADescryptor(ReadOnlySpan<char> privateKeyPem, ReadOnlySpan<char> password, int keySize, RSAEncryptionPadding? padding = null) : this(keySize, padding)
    {
        _rsa.ImportFromEncryptedPem(privateKeyPem, password);
    }

    internal RSADescryptor(int keySize, ReadOnlySpan<byte> pkcs8PrivateKey, RSAEncryptionPadding? padding = null) : this(keySize, padding)
    {
        _rsa.ImportPkcs8PrivateKey(pkcs8PrivateKey, out _);
    }

    internal RSADescryptor(int keySize, ReadOnlySpan<byte> pkcs8PrivateKey, ReadOnlySpan<byte> password, RSAEncryptionPadding? padding = null) : this(keySize, padding)
    {
        _rsa.ImportEncryptedPkcs8PrivateKey(password, pkcs8PrivateKey, out _);
    }

    internal RSADescryptor(int keySize, ReadOnlySpan<byte> pkcs8PrivateKey, ReadOnlySpan<char> password, RSAEncryptionPadding? padding = null) : this(keySize, padding)
    {
        _rsa.ImportEncryptedPkcs8PrivateKey(password, pkcs8PrivateKey, out _);
    }

    private readonly RSACryptoServiceProvider _rsa;
    private readonly RSAEncryptionPadding _padding;

    /// <summary>
    /// 암호화된 데이터를 복호화합니다.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public byte[] Decrypt(byte[] data)
    {
        return _rsa.Decrypt(data, _padding);
    }

    public void Dispose()
    {
        _rsa.Dispose();
    }
}