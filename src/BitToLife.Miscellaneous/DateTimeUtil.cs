namespace BitToLife.Miscellaneous;

/// <summary>
/// 날짜시간 관련 유틸리티
/// </summary>
public static class DateTimeUtil
{
    private const string KOREA_STANDARD_TIME_ZOME_ID = "Korea Standard Time";

    private static TimeZoneInfo GetTimeZone(string id)
    {
        return TimeZoneInfo.FindSystemTimeZoneById(id);
    }

    /// <summary>
    /// 한국표준시(KST) 또는 타임존이 없는 한국시를 협정세계시(UTC)로 변환합니다.
    /// </summary>
    /// <param name="kst">한국표준시(KST)</param>
    /// <returns><see cref="DateTimeOffset"/> 협정세계시(UTC)</returns>
    public static DateTimeOffset KstToUtc(this DateTimeOffset kst)
    {
        TimeSpan kstOffset = GetTimeZone(KOREA_STANDARD_TIME_ZOME_ID).BaseUtcOffset;
        if (kst.Offset.CompareTo(kstOffset) == 0)
        {
            return kst.ToUniversalTime();
        }
        else
        {
            return new DateTimeOffset(kst.Ticks, kstOffset).ToUniversalTime();
        }
    }

    /// <summary>
    /// 협정세계시(UTC)를 한국표준시(KST)로 변환합니다.
    /// </summary>
    /// <param name="utc">협정세계시(UTC)</param>
    /// <returns><see cref="DateTimeOffset"/> 한국표준시(KST)</returns>
    public static DateTimeOffset UtcToKst(this DateTimeOffset utc)
    {
        return TimeZoneInfo.ConvertTime(utc, GetTimeZone(KOREA_STANDARD_TIME_ZOME_ID));
    }

    /// <summary>
    /// 두 날짜 사이의 월 수를 반환합니다.
    /// </summary>
    /// <param name="from">시작날짜</param>
    /// <param name="to">종료날짜</param>
    /// <param name="includeStartMonth">시작월을 포함할지 여부</param>
    /// <returns><see cref="int"/> 월 수</returns>
    public static int GetMonths(DateTimeOffset from, DateTimeOffset to, bool includeStartMonth = false)
    {
        if (from > to)
        {
            (from, to) = (to, from);
        }

        int months = (to.Year - from.Year) * 12 + to.Month - from.Month;

        if (includeStartMonth)
        {
            months++;
        }

        return months;
    }
}
