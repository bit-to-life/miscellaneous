using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace BitToLife.Miscellaneous.Image;

public sealed partial record ImageBuilder
{
    public sealed record ResizeTask : IImageBuilderTask
    {
        public int MaxWidth { get; init; }

        public int MaxHeight { get; init; }

        public SizeType SizeType { get; init; }

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

        public void Execute(SixLabors.ImageSharp.Image image)
        {
            switch (SizeType)
            {
                case SizeType.Contain:
                    (int Width, int Height) = GetContainSize(
                        image.Width,
                        image.Height,
                        MaxWidth,
                        MaxHeight
                    );
                    image.Mutate(i => i.Resize(Width, Height));
                    break;
                case SizeType.Cover:
                    (int CoverWidth, int CoverHeight, int X, int Y) = GetCoverSize(
                        image.Width,
                        image.Height,
                        MaxWidth,
                        MaxHeight
                    );
                    image.Mutate(i =>
                        i.Resize(CoverWidth, CoverHeight)
                            .Crop(new Rectangle(X, Y, MaxWidth, MaxHeight))
                    );
                    break;
                case SizeType.Fill:
                    image.Mutate(i => i.Resize(MaxWidth, MaxHeight));
                    break;
            }
        }
    }
}
