using System.IO.Compression;
using System.Text;

namespace BitToLife.Miscellaneous;

/// <summary>
/// GZip을 이용한 압축/해제 유틸리티
/// </summary>
public static class CompressionUtil
{
    /// <summary>
    /// 데이터를 압축합니다.
    /// </summary>
    /// <param name="source">소스 데이터</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="byte[]"/> 압축된 데이터</returns>
    public async static Task<byte[]> CompressAsync(byte[] source, CancellationToken cancellationToken = default)
    {
        using (MemoryStream memory = new())
        using (GZipStream gzip = new(memory, CompressionMode.Compress))
        {
            await gzip.WriteAsync(source, cancellationToken);
            await gzip.FlushAsync(cancellationToken);

            return memory.ToArray();
        }
    }

    /// <summary>
    /// 압축된 데이터를 해제합니다.
    /// </summary>
    /// <param name="compressedSource">압축된 데이터</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="byte[]"/> 원래 데이터</returns>
    public async static Task<byte[]> DecompressAsync(byte[] compressedSource, CancellationToken cancellationToken = default)
    {
        using (MemoryStream resultMemory = new())
        using (MemoryStream memory = new(compressedSource))
        using (GZipStream gzip = new(memory, CompressionMode.Decompress))
        {
            await gzip.CopyToAsync(resultMemory, cancellationToken);

            return resultMemory.ToArray();
        }
    }

    /// <summary>
    /// 문자열을 압축합니다.
    /// </summary>
    /// <param name="source">소스 문자열</param>
    /// <param name="encoding">인코딩. 기본값은 UTF-8</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="byte[]"/> 압축된 문자열 데이터</returns>
    public async static Task<byte[]> CompressTextAsync(string sourceText, Encoding? encoding = null, CancellationToken cancellationToken = default)
    {
        byte[] source = (encoding ?? Encoding.UTF8).GetBytes(sourceText);
        byte[] compressed = await CompressAsync(source, cancellationToken);
        return compressed;
    }

    /// <summary>
    /// 압축된 문자열을 해제합니다.
    /// </summary>
    /// <param name="compressedText">압축된 문자열 데이터</param>
    /// <param name="encoding">인코딩. 기본값은 UTF-8</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="string"/> 원래 문자열</returns>
    public async static Task<string> DecompressTextAsync(byte[] compressedText, Encoding? encoding = null, CancellationToken cancellationToken = default)
    {
        byte[] source = await DecompressAsync(compressedText, cancellationToken);
        string text = (encoding ?? Encoding.UTF8).GetString(source);
        return text;
    }
}
