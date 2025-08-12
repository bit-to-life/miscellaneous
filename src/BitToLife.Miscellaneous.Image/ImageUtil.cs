using SixLabors.ImageSharp.Formats.Jpeg;
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

    internal static (int Width, int Height) GetContainSize(
        int imageWidth,
        int imageHeight,
        int maxWidth,
        int maxHeight
    )
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

    internal static (int Width, int Height, int X, int Y) GetCoverSize(
        int imageWidth,
        int imageHeight,
        int maxWidth,
        int maxHeight
    )
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

    public static async Task<(int Width, int Height)> GetSizeAsync(
        Stream imageStream,
        CancellationToken cancellationToken = default
    )
    {
        using (
            SixLabors.ImageSharp.Image image = await SixLabors.ImageSharp.Image.LoadAsync(
                imageStream,
                cancellationToken
            )
        )
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
        return new ImageBuilder { Source = source };
    }

    private static async Task<Stream> SaveAsync(
        string path,
        ImageBuilder builder,
        CancellationToken cancellationToken = default
    )
    {
        using (builder.Source)
        {
            builder.Source.Position = 0;
            using (
                SixLabors.ImageSharp.Image image = await SixLabors.ImageSharp.Image.LoadAsync(
                    builder.Source,
                    cancellationToken
                )
            )
            {
                foreach (IImageBuilderTask task in builder.Tasks)
                {
                    task.Execute(image);
                }

                FileStream dest = new(path, FileMode.Create, FileAccess.ReadWrite);
                await image.SaveAsync(
                    dest,
                    builder.Encoder ?? new JpegEncoder(),
                    cancellationToken
                );

                if (builder.OnCompletedAsync is not null)
                {
                    await builder.OnCompletedAsync.Invoke(dest, cancellationToken);
                }

                return dest;
            }
        }
    }

    public static async Task<long> SaveAsync(
        this ImageBuilder builder,
        string? path = null,
        CancellationToken cancellationToken = default
    )
    {
        bool isTempFile = string.IsNullOrWhiteSpace(path);
        string outputPath = isTempFile ? Path.GetTempFileName() : path!;

        using Stream dest = await SaveAsync(outputPath, builder, cancellationToken);
        long length = dest.Length;
        dest.Close();

        if (isTempFile)
        {
            File.Delete(outputPath);
        }

        return length;
    }

    public static async Task<Stream> SaveToStreamAsync(
        this ImageBuilder builder,
        CancellationToken cancellationToken = default
    )
    {
        Stream stream = await SaveAsync(Path.GetTempFileName(), builder, cancellationToken);
        stream.Position = 0;

        return stream;
    }
}
