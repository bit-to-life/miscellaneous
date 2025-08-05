using BitToLife.Miscellaneous.Image;

ImageBuilder builder = ImageUtil
    .CreateBuilder(new FileStream("sample.jpg", FileMode.Open))
    .AddOrientationTask()
    .AddRotateTask(90)
    .AddResizeTask(100, 100, SizeType.Contain)
    .AddRotateTask(90)
    .SetPngEncoder()
    .SetOnCompleted(
        async (stream, cancellationToken) =>
        {
            await Task.Delay(1000, cancellationToken);
        }
    );

await builder.SaveAsync("result.png");

Console.WriteLine("Image processing completed successfully.");
Console.WriteLine("Press any key to exit...");
Console.ReadLine();
