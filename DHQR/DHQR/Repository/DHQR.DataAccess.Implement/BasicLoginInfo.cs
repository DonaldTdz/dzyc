using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.BasicLib;
using DHQR.DataAccess.Entities;

namespace DHQR.DataAccess.Implement
{
    public static class BasicLoginInfo
    { /// <summary>
        /// 获取用户名
        /// </summary>
        /// <returns></returns>
        public static string GetUserName()
        {
            return LoginUserInfo.GetUserName();
        }
        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <returns></returns>
        public static string GetUserKey()
        {
            return LoginUserInfo.GetUserKey();
        }

        /// <summary>
        /// 获取当前用户所有信息
        /// </summary>
        /// <returns></returns>
        public static User GetCurrentUser()
        {
            DHQREntities entity = new DHQREntities();
            UserRepository rep = new UserRepository(entity);
            var logonName = GetUserName();
            User user = rep.GetByLoginName(logonName);
            return user;
        }


    }
}
