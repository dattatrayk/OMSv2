using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Reflection;

namespace OMSv2.Service.Helpers
{
    public static class Utility
    {

        public static string GetSystemDayName(int day)
        {
            string dayOfWeek = ((DayOfWeek)day).ToString();
            return dayOfWeek;
        }

        public static int GetDeductDays(DateTime invoiceDate, DateTime nextInvoiceON)
        {
            int invoiceDateMntsDays = DateTime.DaysInMonth(invoiceDate.Year, invoiceDate.Month);
            int nextInvoiceONMntsDays = DateTime.DaysInMonth(nextInvoiceON.Year, nextInvoiceON.Month);
            // This condition we have tested but still can have some issue which need to be fixed based on condition to condition
            if (invoiceDateMntsDays == 31 && nextInvoiceONMntsDays == 30 && nextInvoiceON.Day == 30)
                return 0;
            return -1;
            //invoiceDate.
        }

        public static int GetCalculateDays(DateTime start, DateTime finish)
        {
            var date1 = start.Day == 31 ? 30 : start.Day;
            var date2 = finish.Day == 31 && (start.Day == 30 || start.Day == 31) ? 30 : finish.Day;

            var actualDaysDiff = (finish - start).TotalDays;

            if (start.Month == 2 || finish.Month == 2)// In case of Feb Month, there will be some calculation issue, so need to handle it.
            {
                // If starting from any day and ending in another month.
                if (finish.Month == (start.Month + 1) && start.Day == (finish.Day + 1))
                    return 30;

                if (actualDaysDiff < 28) // Only take days portion of difference
                    return Convert.ToInt32(Math.Floor(actualDaysDiff));
                else
                {
                    // Starting from Jan 31 and ending at Feb 28 is complete month which is 30 days.
                    if (start.Date.Day == DateTime.DaysInMonth(start.Date.Year, start.Date.Month) &&
                        finish.Date.Day == DateTime.DaysInMonth(finish.Date.Year, finish.Date.Month))
                        return 30;
                }
            }

            int newDaysDiff = 0;
            if (actualDaysDiff > 28)
                newDaysDiff = ((360 * (finish.Year - start.Year)) + (30 * (finish.Month - start.Month)) + (date2 - date1)) + 1;
            else
                newDaysDiff = Convert.ToInt32(Math.Floor(actualDaysDiff));//((360 * (finish.Year - start.Year)) + (30 * (finish.Month - start.Month)) + (date2 - date1));

            if (start.Day == 1 && finish.Day == 31)
                newDaysDiff -= 1;

            return newDaysDiff;



            // ---------------start wrong code----------------
            //double newDaysDiff = 0;
            //var actualDaysDiff = newDaysDiff = (finish - start).TotalDays;
            //if (date1 == 31 || date2 == 31)
            //{
            //    // In case of 31 days month we have to consider 30 days only.
            //    // TODO: We need to brain storm this logic and apply best possible later as an when case arrieses
            //    newDaysDiff = ((360 * (finish.Year - start.Year)) + (30 * (finish.Month - start.Month)) + (date2 - date1)) + 1;
            //}

            //if (start.Day == 1 && finish.Day == 31)
            //    newDaysDiff -= 1;

            //return Convert.ToInt32(newDaysDiff);
            //------------------end wrong code----------------------




            //var endOnFirstDayOfMonth = new DateTime(finish.Year, finish.Month, 1);
            //var endOnLastDayOfMonth = endOnFirstDayOfMonth.AddMonths(1).AddDays(-1);

            //var startOnFirstDayOfMonth = new DateTime(start.Year, start.Month, 1);
            //var startOnLastDayOfMonth = startOnFirstDayOfMonth.AddMonths(1).AddDays(-1);

            ////rentalDaysDetail.CheckInOnString = rentalDaysDetail.CheckInOn.ToCompanyDateTimeFormat();
            //int days = Convert.ToInt32((finish - start).TotalDays);
            //int nofDaysInMnt = DateTime.DaysInMonth(finish.Year, finish.Month);

            //if (days < 29 || (start.Day == 1 && endOnLastDayOfMonth.Day == finish.Day))
            //{
            //    if (days >= 29 && nofDaysInMnt < 30)
            //        days += 1;
            //}
            //else
            //{
            //    days = CalculateDays(start, finish);
            //}
            //return days;
        }

        //private static int CalculateDays(DateTime start, DateTime finish)
        //{
        //    var date1 = start.Day == 31 ? 30 : start.Day;
        //    var date2 = finish.Day == 31 && (start.Day == 30 || start.Day == 31) ? 30 : finish.Day;

        //    var actualDaysDiff = (finish - start).TotalDays;
        //    int newDaysDiff = ((360 * (finish.Year - start.Year)) + (30 * (finish.Month - start.Month)) + (date2 - date1)) + 1;

        //    return newDaysDiff;
        //}

        public static int GetRentalDaysWithGracePeriodDayUnit(DateTime startOn, DateTime endOn, int gracePeriodDailyRental, int gracePeriodWeeklyRental)
        {
            TimeSpan timeDiff = endOn.Subtract(startOn);
            var rentalDays = timeDiff.Days;

            if (rentalDays < 7) // Means daily rental 
            {
                if (timeDiff.Hours > 0 || timeDiff.Minutes > 0) // Means additional hrs are consumed, now need to check weather to charge or not.
                {
                    if (gracePeriodDailyRental > 0)
                    {
                        if (timeDiff.Hours > gracePeriodDailyRental)
                        {
                            return rentalDays + 1;
                        }
                    }
                    else
                    {
                        return rentalDays + 1;
                    }
                }
            }
            else if (rentalDays >= 7 && rentalDays < 5600)
            {
                // Means additional hrs are consumed, now need to check weather to charge or not.
                if (timeDiff.Hours > 0 || timeDiff.Minutes > 0)
                {
                    // Check for grace period.
                    if (gracePeriodWeeklyRental > 0)
                    {
                        if (timeDiff.Hours > gracePeriodWeeklyRental)
                            // Exceed grace period add 1 days charge.
                            return (rentalDays + 1);
                    }
                    else
                    {
                        return rentalDays + 1;
                    }
                }

                return rentalDays;
            }
            //else if (rentalDays >= 29) // In case of Monthly rental, Consider day of booking also, in case of weekly and daily it 24 hrs but for monthly it is considered as days.
            //{
            //    return rentalDays;
            //}

            if (rentalDays <= 0)
                return 1;

            return rentalDays;
        }

        public static int GetRentalDaysWithGracePeriod(DateTime startOn, DateTime endOn, int gracePeriodDailyRental, int gracePeriodWeeklyRental)
        {
            TimeSpan timeDiff = endOn.Subtract(startOn);
            var rentalDays = GetCalculateDays(startOn, endOn);

            if (rentalDays < 7) // Means daily rental 
            {
                if (timeDiff.Hours > 0) // Means additional hrs are consumed, now need to check weather to charge or not.
                {
                    if (gracePeriodDailyRental > 0)
                    {
                        if (timeDiff.Hours > gracePeriodDailyRental)
                        {
                            return rentalDays + 1;
                        }
                    }
                    else
                    {
                        return rentalDays + 1;
                    }
                }
            }
            else if (rentalDays >= 7 && rentalDays < 29)
            {
                // Double check whether this is really Weekly rental, if its monthly add 1 day and return
                //TimeSpan dayDiff = endOn.Date.Subtract(startOn.Date);
                if (rentalDays >= 29)
                    return rentalDays + 1;

                // Check for grace period.
                if (gracePeriodWeeklyRental > 0 && timeDiff.Hours > gracePeriodWeeklyRental)
                {
                    // Exceed grace period add 1 days charge.
                    return (rentalDays + 1);
                }
                return rentalDays;
            }
            else if (rentalDays >= 29) // In case of Monthly rental, Consider day of booking also, in case of weekly and daily it 24 hrs but for monthly it is considered as days.
            {
                return rentalDays;
            }

            if (rentalDays <= 0)
                return 1;

            return rentalDays;
        }

        public static string GetEnumDescription<T>(T value)
        {
            if (value != null)
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());
                if (fi == null)
                    return string.Empty;

                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                    return attributes[0].Description;
                else
                    return value.ToString();
            }
            else
                return string.Empty;
        }

        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
        }

        public static int GetAge(DateTime date)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - date.Year;
            if (now < date.AddYears(age))
                age--;
            return age;
        }

        public static int GetMonthDifference(DateTime fromDate, DateTime toDate)
        {
            return Math.Abs((fromDate.Month - toDate.Month) + 12 * (fromDate.Year - toDate.Year));
        }

        public static Random random = new Random();

        public static object HttpContext { get; private set; }

        public static int GetRandomNumber()
        {
            return random.Next(1, 9999); ;
        }

        public static bool IsValidDateTime(DateTime? dateTime)
        {
            if (dateTime == null || dateTime == DateTime.MinValue || dateTime == DateTime.MinValue.Date)
                return false;
            return true;
        }
        public static bool IsInvalidDateTime(DateTime dateTime)
        {
            return !IsValidDateTime(dateTime);
        }
        public static bool IsValidGuid(Guid? guid)
        {
            if (guid != null && guid != Guid.Empty)
                return true;
            return false;
        }

        public static bool IsValidGuid(string guid)
        {
            return IsValidGuid(Guid.Parse(guid));
        }

        public static bool IsInvalidGuid(Guid? guid)
        {
            return !IsValidGuid(guid);
        }

        public static int IsDefined<T>(int id, T defaultValue)
        {
            if (!Enum.IsDefined(typeof(T), id))
                return Convert.ToInt16(defaultValue);
            else
                return id;
        }
        public static T GetEnumValueByText<T>(string text, T defaultValue)
        {
            if (string.IsNullOrEmpty(text))
                return defaultValue;

            try
            {
                return (T)Enum.Parse(typeof(T), text, true);
            }
            catch
            {
                return defaultValue;
            }
        }
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
        private static string ParseString(object obj, string defaultValue = "")
        {
            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToString(obj);
            }

            return defaultValue;
        }

        public static TimeZoneInfo GetTimeZoneValueById(string timeZoneId)
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

        public static DateTime ConvertToLocalTime(DateTime timeStamp, string timeZoneID)
        {
            timeStamp = DateTime.SpecifyKind(timeStamp, DateTimeKind.Unspecified);
            DateTime localTime = DateTime.Now;
            if (timeStamp != DateTime.MinValue)
            {
                localTime = TimeZoneInfo.ConvertTimeFromUtc(timeStamp, Utility.GetTimeZoneValueById(timeZoneID));
            }
            return localTime;
        }


        public static double ApplyDisc(double rate, double disc)
        {
            return rate - (rate / 100 * disc);
        }

        public static string GetExcelColumnByCount(int column)
        {
            string columnString = "";
            decimal columnNumber = column;
            while (columnNumber > 0)
            {
                decimal currentLetterNumber = (columnNumber - 1) % 26;
                char currentLetter = (char)(currentLetterNumber + 65);
                columnString = currentLetter + columnString;
                columnNumber = (columnNumber - (currentLetterNumber + 1)) / 26;
            }
            return columnString;
        }

        public static bool IsValidEmailID(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

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
                    list.Add(Convert.ToInt32(item));
                }
            }
            return list;
        }
        public static List<Guid> GetListGuidFromStringObject(string data)
        {
            List<Guid> list = new List<Guid>();
            List<string> listInString = data.Split(',').ToList();
            if (listInString != null && listInString.Count > 0)
            {
                foreach (var item in listInString)
                {
                    if (Utility.IsValidGuid(ParseGuid(item)))
                        list.Add(ParseGuid(item));
                }
            }
            return list;
        }

        public static bool IsValidTimeSpan(TimeSpan? timeSpan)
        {
            if (timeSpan != null
                && timeSpan > TimeSpan.Zero)
            {
                return true;
            }
            return false;
        }
    }
}
