using SixLabors.ImageSharp.Processing;

namespace BitToLife.Miscellaneous.Image;

public sealed partial record ImageBuilder
{
    public sealed record RotateTask : IImageBuilderTask
    {
        public float Degree { get; init; }

        public void Execute(SixLabors.ImageSharp.Image image)
        {
            image.Mutate(x => x.Rotate(Degree));
        }
    }
}
