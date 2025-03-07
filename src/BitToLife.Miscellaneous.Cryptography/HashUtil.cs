using System.Security.Cryptography;
using System.Text;

namespace BitToLife.Miscellaneous.Cryptography;

/// <summary>
/// 해시 유틸리티
/// </summary>
public static class HashUtil
{
    private static HashAlgorithm SelectHashAlgorithm(HashAlgorithmType? hashAlgorithmType)
    {
        HashAlgorithm hashAlgorithm = hashAlgorithmType switch
        {
            HashAlgorithmType.SHA384 => SHA384.Create(),
            HashAlgorithmType.SHA512 => SHA512.Create(),
            //HashAlgorithmType.SHA3_256 => SHA3_256.Create(),
            //HashAlgorithmType.SHA3_384 => SHA3_384.Create(),
            //HashAlgorithmType.SHA3_512 => SHA3_512.Create(),
            _ => SHA256.Create()
        };

        return hashAlgorithm;
    }

    /// <summary>
    /// 스트림으로부터 해시값을 계산합니다.
    /// </summary>
    /// <param name="source">스트림</param>
    /// <param name="hashAlgorithmType">해시알고리즘타입</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="byte[]"/> 해시값</returns>
    public static async Task<byte[]> ComputeHashAsync(Stream source, HashAlgorithmType? hashAlgorithmType = null, CancellationToken cancellationToken = default)
    {
        HashAlgorithm hashAlgorithm = SelectHashAlgorithm(hashAlgorithmType);
        byte[] hash = await hashAlgorithm.ComputeHashAsync(source, cancellationToken);

        return hash;
    }

    /// <summary>
    /// 바이트 배열로부터 해시값을 계산합니다.
    /// </summary>
    /// <param name="source">바이트 배열</param>
    /// <param name="hashAlgorithmType">해시알고리즘타입</param>
    /// <returns><see cref="byte[]"/> 해시값</returns>
    public static byte[] ComputeHash(byte[] source, HashAlgorithmType? hashAlgorithmType = null)
    {
        HashAlgorithm hashAlgorithm = SelectHashAlgorithm(hashAlgorithmType);
        byte[] hash = hashAlgorithm.ComputeHash(source);

        return hash;
    }

    /// <summary>
    /// 문자열로부터 해시값을 계산합니다.
    /// </summary>
    /// <param name="source">문자열</param>
    /// <param name="hashAlgorithmType">해시알고리즘타입</param>
    /// <returns><see cref="string"/> 16진수 문자열</returns>
    public static byte[] ComputeHash(string source, HashAlgorithmType? hashAlgorithmType = null)
    {
        byte[] hash = ComputeHash(Encoding.UTF8.GetBytes(source), hashAlgorithmType);

        return hash;
    }

    /// <summary>
    /// 바이트 배열을 16진수 문자열로 변환합니다.
    /// </summary>
    /// <param name="hash">해시값</param>
    /// <returns><see cref="string"/> 16진수 문자열</returns>
    public static string ToHexString(ReadOnlySpan<byte> hash, bool lowercase = false)
    {
        string hexString = Convert.ToHexString(hash);

        if (lowercase)
        {
            return hexString.ToLower();
        }
        else
        {
            return hexString;
        }
    }
}
