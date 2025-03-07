using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;

namespace BitToLife.Miscellaneous.Image;

public static class ImageBuilderExtensions
{
    public static ImageBuilder SetOrientation(this ImageBuilder builder, bool autoFix = true)
    {
        if (builder.Orientation is not null)
        {
            throw new InvalidOperationException("Orientation is already set.");
        }

        builder.Orientation = new ImageBuilder.OrientationOptions
        {
            AutoFix = autoFix
        };

        return builder;
    }

    public static ImageBuilder SetResize(this ImageBuilder builder, int maxWidth, int maxHeight, SizeType sizeType)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxWidth);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxHeight);

        if (builder.Resize is not null)
        {
            throw new InvalidOperationException("Resize is already set.");
        }

        builder.Resize = new ImageBuilder.ResizeOptions
        {
            MaxWidth = maxWidth,
            MaxHeight = maxHeight,
            SizeType = sizeType
        };

        return builder;
    }

    public static ImageBuilder SetRotate(this ImageBuilder builder, float degree)
    {
        if (builder.Rotate is not null)
        {
            throw new InvalidOperationException("Rotate is already set.");
        }

        builder.Rotate = new ImageBuilder.RotateOptions
        {
            Degree = degree
        };

        return builder;
    }

    public static ImageBuilder SetJpegEncoder(this ImageBuilder builder, int quality = 75)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(quality, 100);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quality);

        if (builder.Encoder is not null)
        {
            throw new InvalidOperationException("Encoder is already set.");
        }

        builder.Encoder = new JpegEncoder
        {
            Quality = quality
        };

        return builder;
    }

    public static ImageBuilder SetPngEncoder(this ImageBuilder builder, PngColorType? colorType = null, PngBitDepth? bitDepth = null, PngInterlaceMode? interlaceMode = null)
    {
        if (builder.Encoder is not null)
        {
            throw new InvalidOperationException("Encoder is already set.");
        }

        builder.Encoder = new PngEncoder
        {
            ColorType = colorType.HasValue ? (SixLabors.ImageSharp.Formats.Png.PngColorType)colorType : null,
            BitDepth = bitDepth.HasValue ? (SixLabors.ImageSharp.Formats.Png.PngBitDepth)bitDepth : null,
            InterlaceMethod = interlaceMode.HasValue ? (SixLabors.ImageSharp.Formats.Png.PngInterlaceMode)interlaceMode : null
        };

        return builder;
    }

    public static ImageBuilder SetBmpEncoder(this ImageBuilder builder, BmpBitsPerPixel? bitsPerPixel = null)
    {
        if (builder.Encoder is not null)
        {
            throw new InvalidOperationException("Encoder is already set.");
        }

        builder.Encoder = new BmpEncoder
        {
            BitsPerPixel = bitsPerPixel.HasValue ? (SixLabors.ImageSharp.Formats.Bmp.BmpBitsPerPixel)bitsPerPixel : null
        };

        return builder;
    }

    public static ImageBuilder SetOnCompleted(this ImageBuilder builder, Func<Stream, CancellationToken, Task>? onCompletedAsync = null)
    {
        if (builder.OnCompletedAsync is not null)
        {
            throw new InvalidOperationException("OnCompletedAsync is already set.");
        }

        builder.OnCompletedAsync = onCompletedAsync;

        return builder;
    }
}