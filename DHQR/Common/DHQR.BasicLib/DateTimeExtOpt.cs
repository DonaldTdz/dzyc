using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DHQR.BasicLib
{
    public class DateTimeExtOpt
    {

        

        /// <summary>
        /// 转化时间
        /// </summary>
        /// <param name="timeStr"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(string timeStr,string format)
        {
            if (format == "yyyyMMddHHmmss")
            {
                string str = timeStr.Substring(0, 4) + "-" + timeStr.Substring(4, 2) + "-" + timeStr.Substring(6, 2) + " " + timeStr.Substring(8, 2) + ":" + timeStr.Substring(10, 2) + ":" + timeStr.Substring(12, 2);
                return DateTime.Parse(str);
            }
            else
            {
                return DateTime.ParseExact(timeStr, format, CultureInfo.CurrentCulture, DateTimeStyles.None);
            }

        }


        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"> DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
    }
}
