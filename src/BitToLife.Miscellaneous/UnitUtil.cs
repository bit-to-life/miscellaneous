namespace BitToLife.Miscellaneous;

/// <summary>
/// 단위 변환 유틸리티
/// </summary>
public sealed class UnitUtil
{
    private const float _UNIT_PYEONG = 3.30579f;

    public static class SquareMeter
    {
        /// <summary>
        /// 제곱미터를 평으로 변환
        /// </summary>
        /// <param name="value">제곱미터</param>
        /// <returns><see cref="float"/> 평</returns>
        public static float ConvertToPyeong(float value) => value / _UNIT_PYEONG;

        /// <summary>
        /// 제곱미터를 평으로 변환
        /// </summary>
        /// <param name="value">제곱미터</param>
        /// <returns><see cref="double"/> 평</returns>
        public static double ConvertToPyeong(double value) => value / _UNIT_PYEONG;
    }

    public static class Pyeong
    {
        /// <summary>
        /// 평을 제곱미터로 변환
        /// </summary>
        /// <param name="value">평</param>
        /// <returns><see cref="float"/> 제곱미터</returns>
        public static float ConvertToSquareMeter(float value) => value * _UNIT_PYEONG;

        /// <summary>
        /// 평을 제곱미터로 변환
        /// </summary>
        /// <param name="value">평</param>
        /// <returns><see cref="double"/> 제곱미터</returns>
        public static double ConvertToSquareMeter(double value) => value * _UNIT_PYEONG;
    }
}
