namespace BitToLife.Miscellaneous.Image;

/// <summary>
/// SixLabors.ImageSharp.Formats.Bmp.BmpBitsPerPixel
/// </summary>
public enum BmpBitsPerPixel : short
{
    /// <summary>
    /// 1 bit per pixel.
    /// </summary>
    Pixel1 = 1,

    /// <summary>
    /// 2 bits per pixel.
    /// </summary>
    Pixel2 = 2,

    /// <summary>
    /// 4 bits per pixel.
    /// </summary>
    Pixel4 = 4,

    /// <summary>
    /// 8 bits per pixel. Each pixel consists of 1 byte.
    /// </summary>
    Pixel8 = 8,

    /// <summary>
    /// 16 bits per pixel. Each pixel consists of 2 bytes.
    /// </summary>
    Pixel16 = 16,

    /// <summary>
    /// 24 bits per pixel. Each pixel consists of 3 bytes.
    /// </summary>
    Pixel24 = 24,

    /// <summary>
    /// 32 bits per pixel. Each pixel consists of 4 bytes.
    /// </summary>
    Pixel32 = 32
}
