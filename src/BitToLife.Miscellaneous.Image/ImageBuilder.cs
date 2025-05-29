using SixLabors.ImageSharp.Formats;

namespace BitToLife.Miscellaneous.Image;

public sealed record ImageBuilder
{
    public required Stream Source { get; init; }

    internal OrientationOptions? Orientation { get; set; }

    internal ResizeOptions? Resize { get; set; }

    internal RotateOptions? Rotate { get; set; }

    internal CropOptions? Crop { get; set; }

    internal IImageEncoder? Encoder { get; set; }

    internal Func<FileStream, CancellationToken, Task>? OnCompletedAsync { get; set; }

    public sealed record OrientationOptions
    {
        public bool AutoFix { get; init; }
    }

    public sealed record ResizeOptions
    {
        public int MaxWidth { get; init; }
        public int MaxHeight { get; init; }
        public SizeType SizeType { get; init; }
    }

    public sealed record RotateOptions
    {
        public float Degree { get; init; }
    }

    public sealed record CropOptions
    {
        public int Left { get; init; }
        public int Top { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }
    }

    public sealed record JpegEncodeOptions
    {
        public int Quality { get; init; }
    }
}