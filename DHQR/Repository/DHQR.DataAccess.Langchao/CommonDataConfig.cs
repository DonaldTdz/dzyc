using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DHQR.DataAccess.Langchao
{

    public class CommonDataConfig
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        //public static string ConnectionDefaultStr = "server=grd4-daniel-liu;database=readsystem;uid=sa;pwd=liuandliu";
        public static string ConnectionDefaultStr = ConfigurationManager.AppSettings["ConnStr"];

        /// <summary>
        /// 客户经纬度链接字符串
        /// </summary>
        public static string ConnectionJWStr = ConfigurationManager.AppSettings["ConnJWStr"];

        /// <summary>
        /// 客户二维码链接字符串
        /// </summary>
        public static string ConnectionQRCodeStr = ConfigurationManager.AppSettings["ConnQRCodeStr"];

        /// <summary>
        /// CommonDataConfig
        /// </summary>
        public CommonDataConfig()
        {
            try
            {
                //ConnectionDefaultStr =  ConfigurationSettings.AppSettings["ConnStr"];
                ConnectionDefaultStr = ConfigurationManager.AppSettings["ConnStr"];
                //				if (ConnectionDefaultStr != null)
                //				{
                //					byte[] data = Convert.FromBase64String(ConnectionDefaultStr);
                //					ConnectionDefaultStr = ASCIIEncoding.ASCII.GetString(data);
                //				}
                //				byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(this.textBox1.Text); 
                //				string str = Convert.ToBase64String(data);
            }
            catch
            {
                ConnectionDefaultStr = null;
            }
        }
    }
}
