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
    /// 用户对应功能项数据服务层
    /// </summary>
    public class UserModuleRoleRepository : ProRep<UserModuleRole>
    {
        #region  基础


        public UserModuleRoleRepository()
        {
            ActiveContext = new DHQREntities();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public UserModuleRoleRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<UserModuleRole> EntityCurrentSet
        {
            get { return ActiveContext.UserModuleRoles; }
        }

        protected internal ObjectQuery<UserModuleRole> ObjectQuery
        {
            
            get { return EntityCurrentSet.Include("User").Include("ModuleRole"); }
        }


        #endregion

        #region
        /// <summary>
        /// 通过登录名获取用户的功能项
        /// </summary>
        /// <param name="userName">用户登录名</param>
        /// <returns>功能项清单</returns>
        public IList<Module> GetFeaturesByLogonName(string userName)
        {

            // 当前用户属于管理员角色时，返回所有功能项
            if (userName=="admin")
            {
                return ActiveContext.Modules.ToList();
            }
            //获取用户已分配角色的功能项清单
            ObjectQuery<ModuleRoleToModule> featureRoleFeatures = ActiveContext.ModuleRoleToModules;
            ObjectQuery<UserModuleRole> userFeatureRoles = ActiveContext.UserModuleRoles.Include("User");
            IQueryable<Module> query = from frf in featureRoleFeatures
                                        join ufr in userFeatureRoles on frf.ModuleRoleId equals ufr.ModuleRoleId
                                        where ufr.User.Name == userName
                                        select frf.Module;
            IList<Module> userFeatures = query.ToList();
            return userFeatures;
        }



        #endregion
    }

}
