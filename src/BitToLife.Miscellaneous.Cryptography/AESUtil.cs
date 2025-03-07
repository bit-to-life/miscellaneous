using System.Security.Cryptography;
using System.Text;

namespace BitToLife.Miscellaneous.Cryptography;

/// <summary>
/// AES 암호화 유틸리티
/// </summary>
public static class AESUtil
{
    /// <summary>
    /// 데이터를 암호화합니다.
    /// </summary>
    /// <param name="source">데이터</param>
    /// <param name="secretKey">비밀키 (16바이트 또는 32바이트)</param>
    /// <param name="cipherMode">CipherMode</param>
    /// <param name="paddingMode">PaddingMode</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns><see cref="byte[]"></see> 암호화된 데이터</returns>
    public static async Task<byte[]> EncryptAsync(byte[] source, byte[] secretKey, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfZero(source.Length);

        using (Aes aes = Aes.Create())
        {
            aes.Mode = cipherMode;
            aes.Padding = paddingMode;
            aes.Key = secretKey;

            using (MemoryStream memory = new())
            {
                await memory.WriteAsync(aes.IV, cancellationToken);

                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                using (CryptoStream stream = new(memory, encryptor, CryptoStreamMode.Write))
                {
                    await stream.WriteAsync(source, cancellationToken);
                }

                byte[] binary = memory.ToArray();

                return binary;
            }
        }
    }

    /// <summary>
    /// 암호화된 데이터를 복호화합니다.
    /// </summary>
    /// <param name="encryptedSource">암호화된 데이터</param>
    /// <param name="secretKey">비밀키 (16바이트 또는 32바이트)</param>
    /// <param name="cipherMode">CipherMode</param>
    /// <param name="paddingMode">PaddingMode</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns><see cref="byte[]"/> 복호화된 데이터</returns>
    public static async Task<byte[]> DescryptAsync(byte[] encryptedSource, byte[] secretKey, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfZero(encryptedSource.Length);

        byte[] iv = encryptedSource.AsMemory(0, 16).ToArray();
        byte[] data = encryptedSource.AsMemory(16).ToArray();

        using (Aes aes = Aes.Create())
        {
            aes.Mode = cipherMode;
            aes.Padding = paddingMode;
            aes.Key = secretKey;
            aes.IV = iv;

            using (MemoryStream memory = new())
            {
                using (ICryptoTransform encryptor = aes.CreateDecryptor())
                using (CryptoStream stream = new(memory, encryptor, CryptoStreamMode.Write))
                {
                    await stream.WriteAsync(data, cancellationToken);
                }

                byte[] binary = memory.ToArray();

                return binary;
            }

        }
    }

    /// <summary>
    /// 텍스트를 암호화합니다.
    /// </summary>
    /// <param name="source">텍스트</param>
    /// <param name="secretKey">비밀키 (16바이트 또는 32바이트)</param>
    /// <param name="encoding">인코딩</param>
    /// <param name="cipherMode">CipherMode</param>
    /// <param name="paddingMode">PaddingMode</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns><see cref="string"/> 암호화된 Base64String</returns>
    public static async Task<byte[]> EncryptTextAsync(string source, byte[] secretKey, Encoding? encoding = null, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(source);

        byte[] sourceBinary = (encoding ?? Encoding.UTF8).GetBytes(source);
        byte[] encrypted = await EncryptAsync(sourceBinary, secretKey, cipherMode, paddingMode, cancellationToken);

        return encrypted;
    }

    /// <summary>
    /// 암호화된 Base64String을 복호화합니다.
    /// </summary>
    /// <param name="encryptedText">암호화된 Base64String</param>
    /// <param name="secretKey">비밀키 (16바이트 또는 32바이트)</param>
    /// <param name="encoding">인코딩</param>
    /// <param name="cipherMode">CipherMode</param>
    /// <param name="paddingMode">PaddingMode</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns><see cref="string"/> 복호화된 텍스트</returns>
    public static async Task<string> DescryptTextAsync(byte[] encryptedText, byte[] secretKey, Encoding? encoding = null, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfZero(encryptedText.Length);

        byte[] decrypted = await DescryptAsync(encryptedText, secretKey, cipherMode, paddingMode, cancellationToken);
        string text = (encoding ?? Encoding.UTF8).GetString(decrypted);

        return text;
    }
}
