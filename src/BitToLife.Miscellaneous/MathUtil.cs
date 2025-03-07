namespace BitToLife.Miscellaneous;

/// <summary>
/// 수학 관련 유틸리티
/// </summary>
public static class MathUtil
{
    /// <summary>
    /// 중위값을 계산합니다.
    /// </summary>
    /// <param name="numbers"></param>
    /// <returns></returns>
    public static double Median(IEnumerable<double> numbers)
    {
        double[] numberArray = [.. numbers.OrderBy(n => n)];
        int numberCount = numberArray.Length;
        int middleIndex = numberArray.Length / 2;

        if (numberCount % 2 == 0)
        {
            return (numberArray[middleIndex - 1] + numberArray[middleIndex]) / 2;
        }
        else
        {
            return numberArray[middleIndex];
        }
    }

    /// <summary>
    /// 중위값을 계산합니다.
    /// </summary>
    /// <param name="numbers"></param>
    /// <returns></returns>
    public static float Median(IEnumerable<float> numbers)
    {
        float[] numberArray = [.. numbers.OrderBy(n => n)];
        int numberCount = numberArray.Length;
        int middleIndex = numberArray.Length / 2;

        if (numberCount % 2 == 0)
        {
            return (numberArray[middleIndex - 1] + numberArray[middleIndex]) / 2;
        }
        else
        {
            return numberArray[middleIndex];
        }
    }

    /// <summary>
    /// 중위값을 계산합니다.
    /// </summary>
    /// <param name="numbers"></param>
    /// <returns></returns>
    public static double Median(IEnumerable<long> numbers)
    {
        long[] numberArray = [.. numbers.OrderBy(n => n)];
        int numberCount = numberArray.Length;
        int middleIndex = numberArray.Length / 2;

        if (numberCount % 2 == 0)
        {
            return (numberArray[middleIndex - 1] + numberArray[middleIndex]) / 2D;
        }
        else
        {
            return numberArray[middleIndex];
        }
    }

    /// <summary>
    /// 각도를 라디안으로 변환합니다.
    /// </summary>
    /// <param name="degree"></param>
    /// <returns></returns>
    public static double DegreeToRadian(double degree) => degree * Math.PI / 180.0;

    /// <summary>
    /// 라디안을 각도로 변환합니다.
    /// </summary>
    /// <param name="radian"></param>
    /// <returns></returns>
    public static double RadianToDegree(double radian) => radian * 180.0 / Math.PI;
}
