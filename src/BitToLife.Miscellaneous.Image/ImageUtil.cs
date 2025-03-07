using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using SixLabors.ImageSharp.Processing;

namespace BitToLife.Miscellaneous.Image;

public static class ImageUtil
{
    internal static RotateMode GetRotateMode(ushort orientationValue)
    {
        return orientationValue switch
        {
            // rotated 90 right
            8 => RotateMode.Rotate270,
            // bottoms up
            3 => RotateMode.Rotate180,
            // rotated 90 left
            6 => RotateMode.Rotate90,
            // landscape, do nothing
            _ => 0,
        };
    }

    internal static (int Width, int Height) GetContainSize(int imageWidth, int imageHeight, int maxWidth, int maxHeight)
    {
        float targetRatio = maxWidth / (float)(maxWidth + maxHeight);
        float imageRatio = imageWidth / (float)(imageWidth + imageHeight);

        if (targetRatio > imageRatio)
        {
            maxWidth = Convert.ToInt32((float)maxHeight / imageHeight * imageWidth);
        }
        else
        {
            maxHeight = Convert.ToInt32((float)maxWidth / imageWidth * imageHeight);
        }

        return (maxWidth, maxHeight);
    }

    internal static (int Width, int Height, int X, int Y) GetCoverSize(int imageWidth, int imageHeight, int maxWidth, int maxHeight)
    {
        float imageAspectRatio = imageHeight / (float)imageWidth;
        float targetAspectRatio = maxHeight / (float)maxWidth;

        int coverWidth;
        int coverHeight;
        int x;
        int y;

        if (targetAspectRatio >= imageAspectRatio)
        {
            coverWidth = (int)Math.Ceiling(maxHeight / (double)imageHeight * imageWidth);
            coverHeight = maxHeight;
            x = (coverWidth - maxWidth) / 2;
            y = 0;
        }
        else
        {
            coverWidth = maxWidth;
            coverHeight = (int)Math.Ceiling(maxWidth / (double)imageWidth * imageHeight);
            x = 0;
            y = (coverHeight - maxHeight) / 2;
        }

        return (coverWidth, coverHeight, x, y);
    }

    public static async Task<(int Width, int Height)> GetSizeAsync(Stream imageStream, CancellationToken cancellationToken = default)
    {
        using (SixLabors.ImageSharp.Image image = await SixLabors.ImageSharp.Image.LoadAsync(imageStream, cancellationToken))
        {
            return (image.Width, image.Height);
        }
    }

    public static (int Width, int Height) GetSize(byte[] imageBinary)
    {
        using (SixLabors.ImageSharp.Image image = SixLabors.ImageSharp.Image.Load(imageBinary))
        {
            return (image.Width, image.Height);
        }
    }

    public static ImageBuilder CreateBuilder(Stream source)
    {
        return new ImageBuilder
        {
            Source = source
        };
    }

    public static async Task SaveAsync(this ImageBuilder builder, string? path = null, CancellationToken cancellationToken = default)
    {
        using (builder.Source)
        {
            builder.Source.Position = 0;
            using (SixLabors.ImageSharp.Image image = await SixLabors.ImageSharp.Image.LoadAsync(builder.Source, cancellationToken))
            {
                if (builder.Orientation?.AutoFix ?? false)
                {
                    if (image.Metadata.ExifProfile?.TryGetValue(ExifTag.Orientation, out IExifValue<ushort>? orientation) ?? false)
                    {
                        if (ushort.TryParse(orientation!.Value.ToString(), out ushort orientationValue))
                        {
                            RotateMode rotateMode = GetRotateMode(orientationValue);
                            if (rotateMode != RotateMode.None)
                            {
                                image.Mutate(x => x.Rotate(rotateMode));
                                image.Metadata.ExifProfile?.RemoveValue(ExifTag.Orientation);
                            }
                        }
                    }
                }

                if (builder.Resize is not null)
                {
                    int maxWidth = builder.Resize.MaxWidth;
                    int maxHeight = builder.Resize.MaxHeight;

                    switch (builder.Resize.SizeType)
                    {
                        case SizeType.Contain:
                            (int Width, int Height) = GetContainSize(image.Width, image.Height, maxWidth, maxHeight);
                            image.Mutate(i => i.Resize(Width, Height));
                            break;
                        case SizeType.Cover:
                            (int CoverWidth, int CoverHeight, int X, int Y) = GetCoverSize(image.Width, image.Height, maxWidth, maxHeight);
                            image.Mutate(i => i.Resize(CoverWidth, CoverHeight).Crop(new Rectangle(X, Y, maxWidth, maxHeight)));
                            break;
                        case SizeType.Fill:
                            image.Mutate(i => i.Resize(maxWidth, maxHeight));
                            break;
                    }
                }

                if (builder.Rotate is not null)
                {
                    image.Mutate(x => x.Rotate(builder.Rotate.Degree));
                }

                bool isTempFile = string.IsNullOrWhiteSpace(path);
                string outputPath = isTempFile ? Path.GetTempFileName() : path!;

                using (FileStream dest = new(outputPath, FileMode.Create, FileAccess.ReadWrite))
                {
                    await image.SaveAsync(dest, builder.Encoder ?? new JpegEncoder(), cancellationToken);

                    if (builder.OnCompletedAsync is not null)
                    {
                        await builder.OnCompletedAsync.Invoke(dest, cancellationToken);
                    }
                }

                if (isTempFile)
                {
                    File.Delete(outputPath);
                }
            }
        }
    }
}