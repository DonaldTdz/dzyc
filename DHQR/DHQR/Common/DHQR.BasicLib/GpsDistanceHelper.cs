using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DHQR.BasicLib
{
    /// <summary>
    /// GPS距离帮助类
    /// </summary>
    public class GpsDistanceHelper
    {

        ///计算两点GPS坐标的距离
        /// </summary>
        /// <param name="n1">第一点的纬度坐标</param>
        /// <param name="e1">第一点的经度坐标</param>
        /// <param name="n2">第二点的纬度坐标</param>
        /// <param name="e2">第二点的经度坐标</param>
        /// <returns></returns>
        public static double Distance(double n1, double e1, double n2, double e2)
        {
            double jl_jd = 102834.74258026089786013677476285;
            double jl_wd = 111712.69150641055729984301412873;
            double b = Math.Abs((e1 - e2) * jl_jd);
            double a = Math.Abs((n1 - n2) * jl_wd);
            return Math.Sqrt((a * a + b * b));

        }

        public static double GetDistance(double n1, double e1, double n2, double e2)
        {
          //  d＝111.12cos{1/[sinΦAsinΦB十cosΦAcosΦBcos(λB—λA)]} 
            var result=111.12 * Math.Cos(1/(Math.Sin(n1)*Math.Sin(n2)+Math.Cos(e1)*Math.Cos(e2)));
            return result;
        }

    }
}
