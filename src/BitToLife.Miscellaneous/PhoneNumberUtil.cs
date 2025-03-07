using System.Text.RegularExpressions;

namespace BitToLife.Miscellaneous;

/// <summary>
/// 전화번호 관련 유틸리티
/// </summary>
public static partial class PhoneNumberUtil
{
    public interface ICountryPhoneNumberUtil
    {
        /// <summary>
        /// 전화번호에서 대시(-)를 추가
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        static abstract string WithDash(string phoneNumber);

        /// <summary>
        /// 전화번호에서 뒷자리를 가져옵니다.
        /// </summary>
        /// <param name="phoneNumber">전화번호</param>
        /// <returns></returns>
        static abstract string Suffix(string phoneNumber);
    }

    public abstract partial class CoutryPhoneNumberUtilBase
    {
        [GeneratedRegex("[^0-9]")]
        private static partial Regex NotNumberRegex();

        /// <summary>
        /// 전화번호에서 숫자만 남깁니다.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        protected static string ToNumberOnly(string phoneNumber)
        {
            string p = NotNumberRegex().Replace(phoneNumber, string.Empty);

            return p;
        }

        /// <summary>
        /// 전화번호에서 뒷자리를 가져옵니다.
        /// </summary>
        /// <param name="phoneNumber">전화번호</param>
        /// <param name="length">뒷자리 길이</param>
        /// <returns></returns>
        protected static string Suffix(string phoneNumber, int length)
        {
            string p = ToNumberOnly(phoneNumber);

            if (phoneNumber.Length <= length)
            {
                return p;
            }

            return p[^length..];
        }

        /// <summary>
        /// 전화번호에서 대시(-)를 제거합니다.
        /// </summary>
        /// <param name="phoneNumber">전화번호</param>
        /// <returns></returns>
        public static string WithoutDash(string phoneNumber)
        {
            string p = ToNumberOnly(phoneNumber);

            return p;
        }
    }
}
