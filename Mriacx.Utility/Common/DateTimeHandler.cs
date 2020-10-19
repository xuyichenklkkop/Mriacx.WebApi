using Mriacx.Utility.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace DoCare.Utility.Common
{
    public class DateTimeHandler
    {
        /// <summary>
        /// 当前时间
        /// </summary>
        public static DateTime CurrentTime => DateTime.Now;

        public static DateTime GetMaxDateTime()
        {
            return new DateTime(2100, 1, 1);
        }

        public static DateTime GetMinDateTime()
        {
            return new DateTime(1901, 1, 1);
        }

        /// <summary>
        /// 当前时间（没有秒）
        /// </summary>
        public static DateTime CurrentTimeWithoutSecond => CurrentTime.NoSecond();

        public const string ShortDateTime = "yyyy-MM-dd";

        /// <summary>
        /// yyyy-MM-dd HH:mm:ss
        /// </summary>
        public const string LongDateTimeStyle = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// yyyy-MM-dd HH:mm:ss fff
        /// </summary>
        public const string BigLongDateTimeStyle = "yyyy-MM-dd HH:mm:ss fff";

        /// <summary>
        /// yyyyMMddHHmmss
        /// </summary>
        public const string DateTimeNumberStyle = "yyyyMMddHHmmss";

        /// <summary>
        /// yyyyMMddHHmmssfff
        /// </summary>
        public const string DateTimeFullStyle = "yyyyMMddHHmmssfff";

        /// <summary>
        /// yyyy年MM月dd日 HH时mm分
        /// </summary>
        public const string ShortDateTimeChineseStyle = "yyyy年MM月dd日 HH时mm分";

        /// <summary>
        /// yyyy年MM月dd日
        /// </summary>
        public const string ShortDateChineseStyle = "yyyy年MM月dd日";

        public static DateTime MinTime => new DateTime(1900, 1, 1);

        /// <summary>
        /// 将Json序列化的时间由/Date(1304931520336+0800)/转为字符串
        /// </summary>
        public static string ConvertJsonAfter1970DateToDateString(Match m)
        {
            var dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            var result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        /// <summary>
        /// 将Json序列化的时间由/Date(1304931520336+0800)/转为字符串
        /// </summary>
        public static string ConvertJsonBefore1970DateToDateString(Match m)
        {
            var dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(0 - long.Parse(m.Groups[1].Value));
            if (dt != DateTime.MinValue)
            {
                dt = dt.ToLocalTime();
            }
            var result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        /// <summary>
        /// 获得二个输入日期的之间的每一天的日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<DateTime> GetDayListByDate(DateTime firstDate, DateTime secondDate)
        {
            var days = secondDate.Subtract(firstDate).Days;
            return GetDayList(days, firstDate).ToList();
        }

        /// <summary>
        /// 获得一个输入日期的当月的每一天的日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<DateTime> GetDayListByDate(DateTime date)
        {
            var days = DateTime.DaysInMonth(date.Year, date.Month);
            return GetDayList(days, date.AddDays(-date.Day)).ToList();
        }

        private static IEnumerable<DateTime> GetDayList(int number, DateTime date)
        {
            for (var i = 1; i <= number; i++)
            {
                yield return date.AddDays(i);
            }
        }

        /// <summary>
        /// 判断是否在时间范围内（包头不包尾）
        /// </summary>
        /// <param name="time">时间点</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static bool IsBetween(DateTime time, DateTime startTime, DateTime endTime)
        {
            return time >= startTime && time < endTime;
        }

        /// <summary>
        /// 判断是否在时间范围内（包头包尾）
        /// </summary>
        /// <param name="time">时间点</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static bool IsBetweenAndTial(DateTime time, DateTime startTime, DateTime endTime)
        {
            return time >= startTime && time <= endTime;
        }

        /// <summary>
        /// 计算患者生日（默认生日为1月1号）
        /// </summary>
        /// <param name="dateTime">当前日期</param>
        /// <param name="age">年龄</param>
        /// <param name="month">出生月份</param>
        /// <param name="day">出生日期</param>
        /// <returns></returns>
        public static DateTime CalculateBirthday(DateTime dateTime, int age, int month = 1, int day = 1)
        {
            var date = new DateTime(dateTime.Year - age, month, day);
            return date > MinTime ? date : MinTime;
        }

        /// <summary>
        /// 获取设备同步时间
        /// </summary>
        /// <param name="freq"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetDeviceSyncTime(int freq, DateTime dateTime)
        {
            var time = dateTime.Date.AddHours(dateTime.Hour);
            while (time <= dateTime)
            {
                time = time.AddMinutes(freq <= 0 ? 60 : freq);
            }

            return time;
        }
    }
}
