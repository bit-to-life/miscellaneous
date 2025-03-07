namespace BitToLife.Miscellaneous;

/// <summary>
/// 연속되는 숫자를 생성하는 유틸리티
/// </summary>
public sealed class SequentialNumberUtil
{
    /// <summary>
    /// 새 인스턴스를 생성합니다.
    /// </summary>
    public SequentialNumberUtil() => _number = 0;

    /// <summary>
    /// 새 인스턴스를 생성합니다.
    /// </summary>
    /// <param name="start">시작값</param>
    public SequentialNumberUtil(int start) => Initialize(start);

    private int _number;

    private void Initialize(int start) => _number = start - 1;

    /// <summary>
    /// 다음 숫자를 반환합니다.
    /// </summary>
    /// <returns></returns>
    public int Next() => ++_number;

    /// <summary>
    /// 시퀀스를 초기화합니다.
    /// </summary>
    /// <param name="start">시작값</param>
    public void Reset(int start = 0) => Initialize(start);
}
