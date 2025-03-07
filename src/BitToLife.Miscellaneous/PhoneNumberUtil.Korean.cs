using System.Text.RegularExpressions;

namespace BitToLife.Miscellaneous;

public static partial class PhoneNumberUtil
{
    /// <summary>
    /// 한국 전화번호
    /// </summary>
    public partial class Korean : CoutryPhoneNumberUtilBase, ICountryPhoneNumberUtil
    {
        [GeneratedRegex("(^02.{0}|^01.{1}|^050.{1}|^05.{1}|^07.{1}|[0-9]{3})([0-9]+)([0-9]{4})")]
        private static partial Regex PhoneNumberRegex();

        public static string WithDash(string phoneNumber)
        {
            string p = ToNumberOnly(phoneNumber);

            if (p.Length > 8)
            {
                GroupCollection groups = PhoneNumberRegex().Match(p).Groups;

                return $"{groups[1].Value}-{groups[2].Value}-{groups[3].Value}";
            }
            else
            {
                return $"{p[..4]}-{p[4..8]}";
            }
        }

        public static string Suffix(string phoneNumber)
        {
            return Suffix(phoneNumber, 4);
        }
    }
}
