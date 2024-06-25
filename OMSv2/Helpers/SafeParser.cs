using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OMSv2.Service.Helpers
{
    public class SafeParser
    {
        /// <summary>
        /// get parse string
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string ParseString(object obj, string defaultValue = "")
        {
            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToString(obj).Trim();
            }

            return defaultValue;
        }

        /// <summary>
        /// get pars integer
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns>get default value</returns>
        public static int ParseInteger(object obj, int defaultValue = 0)
        {
            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToInt32(obj);
            }
            return defaultValue;
        }

        /// <summary>
        /// get default value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns>get value</returns>
        public static int IsDefined<T>(object obj, T defaultValue)
        {
            var value = ParseInteger(obj);
            if (!Enum.IsDefined(typeof(T), value))
                return Convert.ToInt16(defaultValue);
            else
                return value;
        }

        /// <summary>
        /// get Parse Decimal
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns>get default value</returns>
        public static decimal ParseDecimal(object obj, decimal defaultValue = 0)
        {
            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToDecimal(obj);
            }
            return defaultValue;
        }


        /// <summary>
        /// get Parse Double
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns>get default value</returns>
        public static double ParseDouble(object obj, double defaultValue = 0)
        {
            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToDouble(obj);
            }
            return defaultValue;
        }

        /// <summary>
        /// get Parse Guid
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>get guid</returns>
        public static Guid ParseGuid(object obj)
        {
            if (obj != null && obj != DBNull.Value)
            {
                var value = ParseString(obj);
                if (!string.IsNullOrEmpty(value))
                    return new Guid(value);
            }
            return new Guid();
        }

        /// <summary>
        /// get Parse Guid Nullable
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>get guid</returns>
        public static Guid? ParseGuidNullable(object obj)
        {
            if (obj != null && obj != DBNull.Value)
            {
                var value = ParseString(obj);
                if (!string.IsNullOrEmpty(value))
                {
                    if (new Guid(value) != Guid.Empty)
                        return new Guid(value);
                }

            }
            return null;
        }

        /// <summary>
        /// get parse bool
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ParseBool(object obj)
        {
            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToBoolean(obj);
            }
            return false;
        }

        /// <summary>
        /// get Parse Byte Array
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ParseByteArray(object obj)
        {
            if (obj != null && obj != DBNull.Value)
                return (byte[])obj;
            return null;
        }

        /// <summary>
        /// convert To Utc
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="localZoneId"></param>
        /// <returns>get datetime</returns>
        public static DateTime? ConvertToUtc(DateTime? datetime, string localZoneId)
        {
            if (datetime == null || datetime.Value == DateTime.MinValue) return null;

            TimeZoneInfo timeZone;
            if (string.IsNullOrEmpty(localZoneId))
            {
                timeZone = TimeZoneInfo.Local;
            }
            else
            {
                timeZone = GetTimeZoneValueById(localZoneId);
            }

            var dateTimeConverted = DateTime.SpecifyKind(Convert.ToDateTime(datetime), DateTimeKind.Unspecified);
            datetime = TimeZoneInfo.ConvertTimeToUtc(Convert.ToDateTime(dateTimeConverted), timeZone);

            return datetime.Value;
        }

        /// <summary>
        /// get parse date
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Convert To Local Time</returns>
        public static DateTime ParseDate(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return DateTime.MinValue.Date;
            }
            return ConvertToLocalTime(Convert.ToDateTime(obj), string.Empty);
        }

        /// <summary>
        /// get parse time span
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>get time span</returns>
        public static TimeSpan ParseTimeSpan(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return new TimeSpan();
            }

            return TimeSpan.Parse(obj.ToString());
        }

        /// <summary>
        /// get parse date nullable
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>convert to date and time</returns>
        public static DateTime? ParseDateNullable(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;

            if (Convert.ToDateTime(obj).Date == DateTime.MinValue.Date)
                return null;

            return Convert.ToDateTime(obj);
        }

        /// <summary>
        /// utc date time
        /// </summary>
        /// <param name="localZoneId"></param>
        /// <returns>convert to utc</returns>
        public static DateTime? UtcDateTimeNow(string localZoneId)
        {
            return ConvertToUtc(DateTime.Now, localZoneId);
        }

        /// <summary>
        /// convert to local time
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="timeZoneId"></param>
        /// <returns>date time</returns>
        public static DateTime ConvertToLocalTime(object obj, string timeZoneId)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return DateTime.MinValue.Date;
            }

            DateTime dateTime = Convert.ToDateTime(obj);
            dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
            DateTime localTime;
            if (string.IsNullOrEmpty(timeZoneId))
            {
                localTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.Local);
            }
            else
            {
                localTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, GetTimeZoneValueById(timeZoneId));
            }
            return localTime;
        }

        /// <summary>
        /// get time zone info
        /// </summary>
        /// <param name="timeZoneId"></param>
        /// <returns>time zone info</returns>
        private static TimeZoneInfo GetTimeZoneValueById(string timeZoneId)
        {
            try
            {
                TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                if (timeZoneInfo != null)
                {
                    return timeZoneInfo;
                }
            }
            catch (TimeZoneNotFoundException)
            {
                return TimeZoneInfo.Local;
            }

            return TimeZoneInfo.Local;
        }
        /// <summary>
        /// Get string from list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetStringfromList<T>(List<T> data)
        {
            if (data == null)
                return string.Empty;

            StringBuilder item = new StringBuilder();
            foreach (var i in data)
            {
                if (!string.IsNullOrEmpty(ParseString(i)))
                    item.Append(i + ",");
            }

            if (!string.IsNullOrEmpty(item.ToString()))
            {
                string withoutLast = item.ToString().Substring(0, (item.Length - 1));
                return withoutLast;
            }
            return string.Empty;
        }
        /// <summary>
        /// get list time span from string
        /// </summary>
        /// <param name="data"></param>
        /// <returns>item</returns>
        public static List<TimeSpan> GetListTimeSpanfromString(string data)
        {
            var item = new List<TimeSpan>();
            if (string.IsNullOrEmpty(data))
                return item;

            var arr = data.Split(',');
            if (arr.Length == 0)
                return item;

            foreach (var i in arr)
            {
                if (!string.IsNullOrEmpty(i.ToString()))
                    item.Add(TimeSpan.Parse(i));
            }
            return item;
        }
        /// <summary>
        /// Get List Int From String Object
        /// </summary>
        /// <param name="data"></param>
        /// <returns>get list</returns>
        public static List<int> GetListIntFromStringObject(string data)
        {
            List<int> list = new List<int>();
            if (string.IsNullOrEmpty(data))
                return list;

            List<string> listInString = data.Split(',').ToList();
            if (listInString != null && listInString.Count > 0)
            {
                foreach (var item in listInString)
                {
                    list.Add(ParseInteger(item));
                }
            }
            return list;
        }
        /// <summary>
        /// get List Guid From String Object 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>get list</returns>
        //public static List<Guid> GetListGuidFromStringObject(string data)
        //{
        //    List<Guid> list = new List<Guid>();
        //    List<string> listInString = data.Split(',').ToList();
        //    if (listInString != null && listInString.Count > 0)
        //    {
        //        foreach (var item in listInString)
        //        {
        //            if (Utility.IsValidGuid(ParseGuid(item)))
        //                list.Add(ParseGuid(item));
        //        }
        //    }
        //    return list;
        //}

        /// <summary>
        /// Get List String From Object
        /// </summary>
        /// <param name="value"></param>
        /// <returns>ge list</returns>
        public static List<string> GetListStringFromObject(object value)
        {
            List<string> list = new List<string>();
            var data = ParseString(value);
            if (string.IsNullOrEmpty(data))
                return list;

            var listInString = data.Split(',');
            if (listInString != null && listInString.Count() > 0)
            {
                foreach (var item in listInString)
                {
                    list.Add(ParseString(item));
                }
            }
            return list;
        }
    }
}
