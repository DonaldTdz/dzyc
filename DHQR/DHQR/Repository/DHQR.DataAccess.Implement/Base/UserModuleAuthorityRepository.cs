using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Common.Base;
using DHQR.DataAccess.Entities;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 用户的功能项权限操作类
    /// </summary>
    public class UserModuleAuthorityRepository
    {
        /// <summary>
        /// 根据登录名获取用户的功能项操作权限
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserModuleAuthority GetByLogonName(string userName)
        {
            //从数据库中获取用户的功能权限
            var result = new UserModuleAuthority
            {
                UserName = userName,
                Modules =
                    new UserModuleRoleRepository().
                    GetFeaturesByLogonName(userName)
            };
            return result;
        }

    }
}
