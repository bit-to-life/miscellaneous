using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace BitToLife.Miscellaneous.Image;

public sealed partial record ImageBuilder
{
    public sealed record CropTask : IImageBuilderTask
    {
        public int Left { get; init; }

        public int Top { get; init; }

        public int Width { get; init; }

        public int Height { get; init; }

        public void Execute(SixLabors.ImageSharp.Image image)
        {
            image.Mutate(x => x.Crop(new Rectangle(Left, Top, Width, Height)));
        }
    }
}
