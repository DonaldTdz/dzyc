using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DHQR.BasicLib
{
    /// <summary>
    /// 特殊转换时间
    /// </summary>
    public static class DateFormatExtention
    {
        /// <summary>
        /// 将格式为yyyyMMdd字符串转换成日期
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(string str)
        {
            Char[] arr = str.ToArray();
            string formatStr = "";
            for (int i = 0; i < 8; i++)
            {
                if (i == 4 || i == 6)
                {
                    formatStr = formatStr + "-" + str[i];
                }
                else
                {
                    formatStr = formatStr + str[i];
                }
            }
            var result = DateTime.Parse(formatStr);
            return result;
        }

    }
}
