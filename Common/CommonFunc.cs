using System;
using System.Globalization;
using System.Text;

namespace Common
{
    public static class CommonFunc
    {
        public static string ChangeTelephoneString(string container, string specialString)
        {
            if (container.Contains(specialString))
            {
                return string.Empty;
            }
            return container;
        }
        
        public static decimal? ChangeZeroToNegative(decimal? source)
        {
            if (!source.Equals(0M))
            {
                return source;
            }
            return null;
        }
        
        public static string CheckIsIntAndConvertToInt(string result)
        {
            if (!string.IsNullOrEmpty(result) && result.EndsWith("00"))
            {
                return result.Remove(result.Length - 3);
            }
            return result;
        }
        
        public static string ConvertBoolToTOrF(bool source)
        {
            string str = "F";
            if (source)
            {
                str = "T";
            }
            return str;
        }
        
        public static DateTime ConvertDateTime<T>(T source)
        {
            DateTime time;
            if ((source != null) && DateTime.TryParse(source.ToString(), out time))
            {
                return time;
            }
            return new DateTime();
        }
        
        public static DateTime? ConvertDateTimeOrNull<T>(T source)
        {
            DateTime time;
            if ((source != null) && DateTime.TryParse(source.ToString(), out time))
            {
                return new DateTime?(time);
            }
            return null;
        }
        
        public static decimal? ConvertObjectToDecimal<T>(T source)
        {
            decimal num;
            if ((source != null) && decimal.TryParse(source.ToString(), out num))
            {
                return new decimal?(num);
            }
            return null;
        }
        
        public static decimal ConvertObjectToDecimalSingle<T>(T source)
        {
            decimal? nullable = ConvertObjectToDecimal<T>(source);
            if (nullable.HasValue)
            {
                return nullable.Value;
            }
            return 0M;
        }
        
        public static int? ConvertObjectToInt<T>(T source)
        {
            int num;
            if ((source != null) && int.TryParse(source.ToString(), out num))
            {
                return new int?(num);
            }
            return null;
        }
        
        public static int ConvertObjectToInt32<T>(T source)
        {
            int num;
            if ((source != null) && int.TryParse(string.Format("{0:f0}", source), out num))
            {
                return num;
            }
            return 0;
        }
        
        public static int? ConvertObjectToNullableInt<T>(T source)
        {
            int num;
            if (source.ToString().Equals(string.Empty))
            {
                return null;
            }
            if (int.TryParse(string.Format("{0:f0}", source), out num))
            {
                return new int?(num);
            }
            return 0;
        }
        
        public static string ConvertObjectToString<T>(T source)
        {
            if (source != null)
            {
                return source.ToString();
            }
            return string.Empty;
        }
        
        public static string ConvertSymbolPercent(string source)
        {
            if (source.EndsWith("%"))
            {
                source = source.Substring(0, source.Length - 1);
                return source;
            }
            source = CovertNBSP(source);
            return source;
        }
        
        public static bool ConvertToBool<T>(T source)
        {
            bool flag;
            if (source == null)
            {
                return false;
            }
            if (source.ToString().ToUpper().Equals("T"))
            {
                return true;
            }
            if (source.ToString().ToUpper().Equals("F"))
            {
                return false;
            }
            return (bool.TryParse(source.ToString().ToLower(), out flag) && flag);
        }
        
        public static byte ConvertToByte<T>(T source)
        {
            byte num;
            if ((source != null) && byte.TryParse(source.ToString(), out num))
            {
                return num;
            }
            return 0;
        }
        
        public static string ConvertToEmptyOrSymbolX(string source)
        {
            if (source.ToLower().Equals("x"))
            {
                return "-1";
            }
            return CovertNBSP(source);
        }
        
        public static V ConvertToEnum<T, V>(T source) where V: new()
        {
            Type enumType = typeof(V);
            V local = (default(V) == null) ? Activator.CreateInstance<V>() : default(V);
            if (source == null)
            {
                return local;
            }
            try
            {
                string str = source.ToString();
                return (V) Enum.Parse(enumType, str, true);
            }
            catch
            {
                return local;
            }
        }
        
        public static int ConvertToFormatInt<T>(T source, int provider)
        {
            try
            {
                return Convert.ToInt32(source.ToString(), provider);
            }
            catch
            {
                return 0;
            }
        }
        
        public static string ConvertToNull<T>(T source)
        {
            decimal? nullable = ConvertObjectToDecimal<T>(source);
            if (!nullable.HasValue)
            {
                return string.Empty;
            }
            return string.Format("{0:F2}", nullable);
        }
        
        public static string ConvertToPercentDecimal<T>(T source)
        {
            if (source == null)
            {
                return string.Empty;
            }
            if (!string.Empty.Equals(source.ToString()))
            {
                decimal num = Convert.ToDecimal(source) / 100M;
                return num.ToString();
            }
            return source.ToString();
        }
        
        public static string ConvertToPercentString<T>(T source)
        {
            if (source == null)
            {
                return string.Empty;
            }
            if (string.Empty.Equals(source.ToString()))
            {
                return source.ToString();
            }
            decimal num = Convert.ToDecimal(source);
            if (num == 0M)
            {
                return "0%";
            }
            return string.Format("{0:#%}", num / 100M);
        }
        
        public static string ConvertToRound<T>(T source)
        {
            if (source == null)
            {
                return string.Empty;
            }
            if (!string.Empty.Equals(source.ToString()))
            {
                return Math.Round(Convert.ToDecimal(source), 2, MidpointRounding.AwayFromZero).ToString();
            }
            return source.ToString();
        }
        
        public static float ConvertToSingle<T>(T source)
        {
            float num;
            if ((source != null) && float.TryParse(source.ToString(), out num))
            {
                return num;
            }
            return 0f;
        }
        
        public static string ConvertToTrimString<T>(T source)
        {
            string str = source.ToString();
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Trim();
            }
            return str;
        }
        
        public static int ConvertToWeek(DayOfWeek week)
        {
            int num = (int) week;
            if (num == 0)
            {
                num = 7;
            }
            return num;
        }
        
        public static int ConvertToWeek<T>(T source)
        {
            DateTime? nullable = ConvertDateTimeOrNull<T>(source);
            int dayOfWeek = 0;
            if (nullable.HasValue)
            {
                dayOfWeek = (int) nullable.Value.DayOfWeek;
                if (dayOfWeek == 0)
                {
                    dayOfWeek = 7;
                }
            }
            return dayOfWeek;
        }
        
        public static string ConvertToZero<T>(T source)
        {
            int? nullable = ConvertObjectToNullableInt<T>(source);
            if (!nullable.HasValue)
            {
                return string.Empty;
            }
            return nullable.ToString();
        }
        
        public static string CovertNBSP(string source)
        {
            if (!source.ToLower().Equals("&nbsp;"))
            {
                return source;
            }
            return string.Empty;
        }
        
        public static string GetDateTimeFormat(DateTime date)
        {
            DateTimeFormatInfo dateTimeFormat = CultureInfo.CreateSpecificCulture("zh-cn").DateTimeFormat;
            return date.ToString("yyyy-MM-dd", dateTimeFormat);
        }
        
        public static string GetDateTimeIntlFormat(DateTime date)
        {
            DateTimeFormatInfo dateTimeFormat = CultureInfo.CreateSpecificCulture("en").DateTimeFormat;
            return date.ToString("MMM-dd-yyyy", dateTimeFormat);
        }
        
        public static int GetDayOfWeek(DateTime dt)
        {
            if (dt.DayOfWeek != DayOfWeek.Sunday)
            {
                return (int) dt.DayOfWeek;
            }
            return 7;
        }
        
        public static string IsDefaultOrF<T>(T source)
        {
            string str = ConvertObjectToString<T>(source);
            if (!string.Empty.Equals(str))
            {
                return str;
            }
            return "F";
        }
        
        public static string IsDefaultOrRMB<T>(T source)
        {
            string str = ConvertObjectToString<T>(source);
            if (!string.Empty.Equals(str))
            {
                return str;
            }
            return "RMB";
        }
        
        public static bool IsNumeric(string str)
        {
            if ((str == null) || (str.Length == 0))
            {
                return false;
            }
            ASCIIEncoding encoding = new ASCIIEncoding();
            foreach (byte num in encoding.GetBytes(str))
            {
                if ((num < 0x30) || (num > 0x39))
                {
                    return false;
                }
            }
            return true;
        }
        
        public static int RoundToInt<T>(T source)
        {
            int num;
            if (source == null)
            {
                return 0;
            }
            if (int.TryParse(source.ToString(), out num))
            {
                return num;
            }
            return ConvertObjectToInt32<decimal>(Math.Floor((decimal) (ConvertObjectToDecimalSingle<T>(source) + 0.5M)));
        }
        
        public static string[] StringSplit(string tmpStr, char separator)
        {
            return tmpStr.Split(new char[] { separator });
        }
    }
}
