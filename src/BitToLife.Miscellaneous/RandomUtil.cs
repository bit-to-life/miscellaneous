namespace BitToLife.Miscellaneous;

/// <summary>
/// 랜던 유틸리티
/// </summary>
public static class RandomUtil
{
    private static readonly ThreadLocal<Random> _random = new(() => new Random());

    public static ThreadLocal<Random> Instance => _random;

    public static int Next() => _random.Value!.Next();

    public static int Next(int maxValue) => _random.Value!.Next(maxValue);

    public static int Next(int minValue, int maxValue) => _random.Value!.Next(minValue, maxValue);

    public static void NextBytes(byte[] buffer) => _random.Value!.NextBytes(buffer);

    public static void NextBytes(Span<byte> buffer) => _random.Value!.NextBytes(buffer);

    public static double NextDouble() => _random.Value!.NextDouble();

    public static double NextDouble(double minValue, double maxValue) => _random.Value!.NextDouble() * (maxValue - minValue) + minValue;

    public static long NextInt64() => _random.Value!.NextInt64();

    public static long NextInt64(long maxValue) => _random.Value!.NextInt64(maxValue);

    public static long NextInt64(long minValue, long maxValue) => _random.Value!.NextInt64(minValue, maxValue);

    public static float NextSingle() => _random.Value!.NextSingle();

    public static float NextSingle(float minValue, float maxValue) => _random.Value!.NextSingle() * (maxValue - minValue) + minValue;

    public static void Shuffle<T>(T[] values) => _random.Value!.Shuffle(values);

    public static void Shuffle<T>(Span<T> values) => _random.Value!.Shuffle(values);
}