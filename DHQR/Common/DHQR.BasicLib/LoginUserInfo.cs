using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.UI.CooKies;
using Common.UI.EncryptTool;


namespace DHQR.BasicLib
{
    public static class LoginUserInfo
    { /// <summary>
        /// 获取用户名
        /// </summary>
        /// <returns></returns>
        public static string GetUserName()
        {
            var cooKies = CooKies.GetCookies("token");
            return EncryptTool.Decrypt(cooKies);
        }
        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <returns></returns>
        public static string GetUserKey()
        {
            var cooKies = CooKies.GetCookies("token_Key");
            return EncryptTool.Decrypt(cooKies);
        }

        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <returns></returns>
        public static Guid GetUserWareHouseId()
        {
            var cooKies = CooKies.GetCookies("token_WareHouseId");
            if (string.IsNullOrEmpty(cooKies))
            {
                return new Guid();
            }
            var str = EncryptTool.Decrypt(cooKies);
            return new Guid(str);
        }
    }
}
