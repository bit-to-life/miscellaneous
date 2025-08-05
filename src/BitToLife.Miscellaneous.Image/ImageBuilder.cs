using SixLabors.ImageSharp.Formats;

namespace BitToLife.Miscellaneous.Image;

public sealed partial record ImageBuilder
{
    public required Stream Source { get; init; }

    internal readonly List<IImageBuilderTask> Tasks = [];

    internal IImageEncoder? Encoder { get; set; }

    internal Func<FileStream, CancellationToken, Task>? OnCompletedAsync { get; set; }

    internal void AddTask(IImageBuilderTask task)
    {
        Tasks.Add(task);
    }
}
