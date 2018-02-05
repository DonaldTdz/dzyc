using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DHQR.BasicLib
{
    /// <summary>
    /// 自动生成单据号、
    /// </summary>
    public class CreateBillNo
    {
        /// 
        /// 单据编号,产生自增编号  
        /// 原值
        /// 下一值
        public static string NewNo(string baseStr)
        {
            var rad = new Random(); //实例化随机数产生器rad；
            int value = rad.Next(1000, 10000);
            return baseStr + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day
                   + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second +
                   value;
        }
    }

}
