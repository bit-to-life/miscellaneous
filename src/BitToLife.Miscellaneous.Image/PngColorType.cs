namespace BitToLife.Miscellaneous.Image;

/// <summary>
/// SixLabors.ImageSharp.Formats.Png.PngColorType
/// </summary>
public enum PngColorType : byte
{
    /// <summary>
    /// Each pixel is a grayscale sample.
    /// </summary>
    Grayscale = 0,

    /// <summary>
    /// Each pixel is an R,G,B triple.
    /// </summary>
    Rgb = 2,

    /// <summary>
    /// Each pixel is a palette index; a PLTE chunk must appear.
    /// </summary>
    Palette = 3,

    /// <summary>
    /// Each pixel is a grayscale sample, followed by an alpha sample.
    /// </summary>
    GrayscaleWithAlpha = 4,

    /// <summary>
    /// Each pixel is an R,G,B triple, followed by an alpha sample.
    /// </summary>
    RgbWithAlpha = 6
}